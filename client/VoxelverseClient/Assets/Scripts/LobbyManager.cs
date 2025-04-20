using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    private TMP_InputField usernameInput;

    void Start()
    {
        usernameInput = FindObjectOfType<TMP_InputField>();
        if (usernameInput == null)
        {
            Debug.LogError("No TMP_InputField found in the scene. Username input will not work.");
        }
    }

    public void OnJoinClicked()
    {
        if (usernameInput == null)
        {
            Debug.LogWarning("Username input field not found.");
            return;
        }

        string username = usernameInput.text.Trim();

        if (!string.IsNullOrEmpty(username))
        {
            PlayerPrefs.SetString("Username", username);
            Debug.Log("Username saved: " + username);

            SceneManager.LoadScene("MainScene"); // Load your main game scene
        }
        else
        {
            Debug.LogWarning("Username is empty!");
        }
    }
}
