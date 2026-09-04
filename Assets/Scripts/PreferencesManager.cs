using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreferencesManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Dropdown wordLengthDropdown;
    public TMP_Dropdown timeDropdown;

    int[] wordLengths = { 3, 5, 8 };
    int[] timeLimits = { 30, 45, 60, 90 };

    void Start()
    {
        playerNameInput.text = "";

        int savedLength = PlayerPrefs.GetInt("wordLength", 3);
        int savedTime = PlayerPrefs.GetInt("timeLimit", 30);

        wordLengthDropdown.value =
            FindIndex(wordLengths, savedLength);

        timeDropdown.value =
            FindIndex(timeLimits, savedTime);
    }

    int FindIndex(int[] values, int savedValue)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == savedValue)
            {
                return i;
            }
        }

        return 0;
    }

    public void SavePreferences()
    {
        string playerName = playerNameInput.text.Trim();

        if (playerName == "")
        {
            playerName = "Player";
        }

        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt(
            "wordLength",
            wordLengths[wordLengthDropdown.value]
        );
        PlayerPrefs.SetInt(
            "timeLimit",
            timeLimits[timeDropdown.value]
        );

        PlayerPrefs.Save();
        SceneManager.LoadScene("wordGameStart");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("intro");
    }
}