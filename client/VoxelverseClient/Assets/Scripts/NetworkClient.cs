using UnityEngine;
using NativeWebSocket;
using System.Text;
using System.Threading.Tasks;

public class NetworkClient : MonoBehaviour
{
    private WebSocket websocket;

    async void Start()
    {
        websocket = new WebSocket("ws://localhost:3000");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error: " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = Encoding.UTF8.GetString(bytes);
            Debug.Log("Message from server: " + message);
        };

        await websocket.Connect();
    }

    void Update()
    {
        if (websocket != null)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue(); // ✅ just call it directly for standalone/desktop builds
#endif
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ = SendWebSocketMessage("Hello from Unity!");
        }
    }

    async Task SendWebSocketMessage(string message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
    }

    async void OnApplicationQuit()
    {
        if (websocket != null)
        {
            await websocket.Close();
        }
    }
}
