using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    public TextMeshProUGUI exitMessageText;

    void Start()
    {
        string playerName =
            PlayerPrefs.GetString("playerName", "Player");

        int score =
            PlayerPrefs.GetInt("score", 0);

        exitMessageText.text =
            "Thanks for playing, " + playerName +
            "!\nFinal Score: " + score;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("intro");
    }
}