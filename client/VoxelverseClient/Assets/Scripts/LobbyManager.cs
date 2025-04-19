using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public TMP_InputField usernameInput;

    public void OnJoinClicked()
    {
        string username = usernameInput.text.Trim();

        if (!string.IsNullOrEmpty(username))
        {
            PlayerPrefs.SetString("Username", username);
            Debug.Log("Username saved: " + username);

            // Load main scene
            SceneManager.LoadScene("MainScene"); 
        }
        else
        {
            Debug.LogWarning("Username is empty!");
        }
    }
}
