# Voxelverse

**Voxelverse** is a distributed multiplayer voxel-based platform inspired by games like **Roblox** and **Minecraft**. Built using Unity and WebSockets, the platform allows players to join different cloud-hosted game servers, communicate in real time, and explore shared voxel-based spaces. 

---

## Features (MVP Scope)

- Real-time multiplayer using WebSockets
- Player-to-player communication (chat system)
- Lobby system for selecting game servers
- Voxel-based test game environment
- Cloud deployment on AWS EC2
- MongoDB Atlas integration
- GitHub-integrated CI/CD pipeline (upcoming)

---

## Platform Overview

- **Type**: Distributed multiplayer voxel game platform
- **Game Engine**: Unity (3D Core + Built-in RP)
- **Multiplayer Server**: Node.js + `ws` WebSocket server
- **Architecture**: Client-server model with EC2-hosted backend and horizontally scalable lobby/server structure
- **Player Controls**: WASD movement, mouse camera, chat, and room selection UI

---

## Tech Stack

| Layer         | Technology                     |
|---------------|-------------------------------|
| Game Engine | Unity                         |
| Backend     | Node.js + `ws` (WebSocket)    |
| Hosting     | AWS EC2 (Free Tier)            |
| Load Balancing | AWS Elastic Load Balancer (ELB) |
| Database   | MongoDB Atlas (GitHub Pack)    |
| CI/CD      | GitHub Actions (planned)       |

---

## Distributed Computing Strategy

- Each backend server runs as a WebSocket instance hosted on AWS EC2.
- Players connect via Unity clients to distributed game servers.
- Load balancing via ELB or manual server join logic allows player distribution.
- MongoDB Atlas stores player/session metadata.
- CI/CD pipeline (coming soon) will auto-deploy and manage updates.

---

## Project Status

| Phase                        | Status          |
|-----------------------------|-----------------|
| GitHub + folder setup     | Complete         |
| Unity project w/ movement | Complete         |
| Raw WebSocket backend     | Complete         |
| Unity-WebSocket connection | In progress      |
| Player sync + chat UI     | In progress      |
| EC2 deployment            | Not yet          |
| MongoDB integration       | Not yet          |
| Lobby UI                  | Not yet          |

---

## Folder Structure

```
voxelverse/
├── client/          # Unity project (3D voxel game)
├── server/          # WebSocket backend in Node.js
├── docs/            # Design notes, architecture
├── README.md
```

---

## Demo

> Demo video and screenshots will be added after final implementation.

---

## Development Log

**April 8–9, 2025**
- GitHub repo created, folder structure finalized
- Unity 3D project setup with working WASD movement
- Backend rewritten using raw WebSocket (`ws`) for Unity compatibility
- `.gitignore` updated for both Unity and Node
- GitHub push successful with client + server

**Common Issues Solved**
- Socket.IO conflict with Unity → Switched to `ws`
- `await void` compiler issue → Used `async Task` wrappers
- Git untracked Unity files → Fixed with proper staging and `.gitignore`

---

## Links

- GitHub Repo: [bareerahanif/voxelverse](https://github.com/bareerahanif/voxelverse)
- Cloud Link: *(to be added after EC2 deployment)*

---

## License

MIT License.
