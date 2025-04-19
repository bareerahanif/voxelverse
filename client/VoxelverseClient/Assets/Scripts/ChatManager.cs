using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text chatDisplay;

    [SerializeField] NetworkClient client;

    void Start()
    {
        client = FindObjectOfType<NetworkClient>();

        if (client == null)
        {
            Debug.LogError("ChatManager: NetworkClient not found in the scene. Chat will not work.");
        }
    }

    public void OnSendButtonClicked()
    {
        string msg = inputField.text.Trim();

        if (!string.IsNullOrEmpty(msg))
        {
            if (client != null)
            {
                client.SendChatMessage(msg);
                inputField.text = "";
            }
            else
            {
                Debug.LogWarning("Tried to send a message but client is null.");
            }
        }
    }

    public void ReceiveChatMessage(string playerId, string msg)
    {
        string fullMessage = $"[{playerId.Substring(0, 4)}]: {msg}";
        chatDisplay.text += "\n" + fullMessage;

        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        Debug.Log("Chat Display Updated: " + fullMessage);
    }

}
