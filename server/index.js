// index.js
const express = require("express");
const http = require("http");
const socketIO = require("socket.io");
const cors = require("cors");

const app = express();
const server = http.createServer(app);
const io = socketIO(server, {
  cors: { origin: "*" }
});

app.use(cors());

io.on("connection", (socket) => {
  console.log("Player connected:", socket.id);

  socket.on("player-move", (data) => {
    socket.broadcast.emit("update-player", { id: socket.id, ...data });
  });

  socket.on("disconnect", () => {
    console.log("Player disconnected:", socket.id);
    socket.broadcast.emit("remove-player", { id: socket.id });
  });
});

server.listen(3000, () => console.log("Server running on http://localhost:3000"));
