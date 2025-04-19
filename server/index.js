const WebSocket = require("ws");
const wss = new WebSocket.Server({ port: 3000 });

const players = {};

wss.on("connection", (ws) => {
  const id = Date.now().toString() + Math.random().toString().slice(2, 6);
  players[id] = { x: 0, y: 0, z: 0 };
  ws.id = id;

  console.log("Player connected:", id);
  ws.send(JSON.stringify({ type: "id", id }));

  // Send info about existing players to the new player
  for (let pid in players) {
    if (pid !== id) {
      ws.send(JSON.stringify({
        type: "player-joined",
        id: pid,
        position: players[pid]
      }));
    }
  }

  // Notify others about the new player (excluding the new one)
  broadcast({
    type: "player-joined",
    id: id,
    position: players[id]
  }, ws);

  ws.on("message", (message) => {
    try {
      const data = JSON.parse(message);

      if (data.type === "move") {
        players[ws.id] = data.position;

        broadcast({
          type: "player-move",
          id: ws.id,
          position: data.position
        }, ws); // skip sender to avoid ghost update
      }

      else if (data.type === "chat") {
        broadcast({
          type: "chat",
          id: ws.id,
          msg: data.msg
        }); // include sender so they see their message
      }

    } catch (err) {
      console.error("Invalid JSON:", message);
    }
  });

  ws.on("close", () => {
    console.log("Player disconnected:", ws.id);
    delete players[ws.id];

    broadcast({
      type: "remove-player",
      id: ws.id
    });
  });
});

// Broadcast message to all (optionally excluding one client)
function broadcast(message, excludeWs = null) {
  const msg = JSON.stringify(message);
  wss.clients.forEach((client) => {
    if (client.readyState === WebSocket.OPEN && client !== excludeWs) {
      client.send(msg);
    }
  });
}

console.log("WebSocket server running on ws://localhost:3000");
