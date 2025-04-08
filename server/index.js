// ws-server.js
const WebSocket = require("ws");

const wss = new WebSocket.Server({ port: 3000 });

wss.on("connection", (ws) => {
  console.log("Client connected");

  ws.on("message", (msg) => {
    console.log("Received:", msg);

    // Echo message back
    ws.send("Echo: " + msg);
  });

  ws.on("close", () => {
    console.log("Client disconnected");
  });
});

console.log("Raw WebSocket server running on ws://localhost:3000");
