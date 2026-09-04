using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "wordGameStart")
        {
            PlayerPrefs.SetInt("score", 0);
        }
    }

    public void StartWordGame()
    {
        SceneManager.LoadScene("wordGame");
    }
    public void OpenStart()
    {
        SceneManager.LoadScene("wordGameStart");
    }

    public void OpenPreferences()
    {
        SceneManager.LoadScene("preferences");
    }

    public void OpenExit()
    {
        SceneManager.LoadScene("exit");
    }
    public void OpenIntro()
    {
        SceneManager.LoadScene("intro");
    }
}