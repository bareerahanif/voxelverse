using UnityEngine;
using NativeWebSocket;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class NetworkClient : MonoBehaviour
{
    static bool alreadyConnected = false;

    WebSocket websocket;
    string myId;
    public GameObject playerPrefab;

    Dictionary<string, GameObject> players = new();
    bool isConnected = false;

    async void Start()
    {
        // Check for prefab assignment
        if (playerPrefab == null)
        {
            Debug.LogWarning("Player Prefab is not assigned. Multiplayer client will not start.");
            return;
        }

        if (alreadyConnected || isConnected)
        {
            Debug.Log("Preventing duplicate WebSocket connection");
            return;
        }

        alreadyConnected = true;

        websocket = new WebSocket("ws://YOUR_SERVER_IP:3000"); // Replace with your actual server IP

        websocket.OnOpen += () =>
        {
            Debug.Log("Connected to server");
            isConnected = true;
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = Encoding.UTF8.GetString(bytes);
            Debug.Log("Incoming: " + message);

            var data = JsonUtility.FromJson<ServerMessage>(message);

            if (data.type == "id")
            {
                myId = data.id;
                Debug.Log("My ID: " + myId);
                return;
            }

            if (string.IsNullOrEmpty(myId)) return;

            if (data.type == "player-joined")
            {
                if (data.id == myId) return;

                if (!players.ContainsKey(data.id))
                {
                    GameObject ghost = Instantiate(playerPrefab);
                    ghost.GetComponent<Renderer>().material.color = Color.red;
                    ghost.transform.position = new Vector3(data.position.x, data.position.y, data.position.z);
                    players[data.id] = ghost;
                    Debug.Log("Ghost spawned: " + data.id);
                }
            }
            else if (data.type == "player-move")
            {
                if (data.id == myId) return;

                if (players.ContainsKey(data.id))
                {
                    players[data.id].transform.position = new Vector3(
                        data.position.x, data.position.y, data.position.z);
                }
            }
            else if (data.type == "remove-player")
            {
                if (players.ContainsKey(data.id))
                {
                    Destroy(players[data.id]);
                    players.Remove(data.id);
                    Debug.Log("Ghost removed: " + data.id);
                }
            }
        };

        websocket.OnError += (e) => Debug.LogError("WebSocket error: " + e);
        websocket.OnClose += (e) =>
        {
            Debug.Log("Disconnected from server");
            isConnected = false;
        };

        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket?.DispatchMessageQueue();
#endif

        if (!string.IsNullOrEmpty(myId))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                SendMove(transform.position);
            }
        }
    }

    async void SendMove(Vector3 pos)
    {
        if (websocket.State == WebSocketState.Open && !string.IsNullOrEmpty(myId))
        {
            var message = new PositionMessage
            {
                type = "move",
                id = myId,
                position = new PositionData
                {
                    x = pos.x,
                    y = pos.y,
                    z = pos.z
                }
            };

            string json = JsonUtility.ToJson(message);
            await websocket.SendText(json);
        }
    }

    async void OnApplicationQuit()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.Close();
        }

        isConnected = false;
        alreadyConnected = false;
    }

    [System.Serializable]
    public class ServerMessage
    {
        public string type;
        public string id;
        public PositionData position;
    }

    [System.Serializable]
    public class PositionMessage
    {
        public string type;
        public string id;
        public PositionData position;
    }

    [System.Serializable]
    public class PositionData
    {
        public float x;
        public float y;
        public float z;
    }
}
