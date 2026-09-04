using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject letter;
    public GameObject center;

    string wordToGuess = "";
    int lengthOfWordToGuess;
    char[] lettersToGuess;
    bool[] lettersGuessed;

    int numberOfAttempts;
    int maximumAttempts;
    int score = 0;

    float timeRemaining;
    TextMeshProUGUI playerNameDisplay;
    TextMeshProUGUI timerDisplay;

    string[] wordsToGuess =
{
    "car", "dog", "sun",
    "apple", "house", "tiger",
    "elephant", "computer"
};

    void Start()
    {
        center = GameObject.Find("centerOfScreen");

        playerNameDisplay = GameObject.Find("playerNameText")
    .GetComponent<TextMeshProUGUI>();

        timerDisplay = GameObject.Find("timerText")
            .GetComponent<TextMeshProUGUI>();

        playerNameDisplay.text =
            PlayerPrefs.GetString("playerName", "Player");

        timeRemaining =
            PlayerPrefs.GetInt("timeLimit", 30);

        numberOfAttempts = 0;
        maximumAttempts = 10;

        InitGame();
        InitLetters();
        UpdateAttempts();
        UpdateScore();
    }
    void Update()
    {
        CheckKeyboard();
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;

        int secondsLeft =
            Mathf.Max(0, Mathf.CeilToInt(timeRemaining));

        timerDisplay.text = "Time: " + secondsLeft;

        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene("wordGameEnd");
        }
    }

    void InitGame()
    {
        int desiredLength =
    PlayerPrefs.GetInt("wordLength", 3);

        int randomNumber;

        do
        {
            randomNumber =
                Random.Range(0, wordsToGuess.Length);

            wordToGuess = wordsToGuess[randomNumber];
        }
        while (wordToGuess.Length != desiredLength);

        lengthOfWordToGuess = wordToGuess.Length;
        wordToGuess = wordToGuess.ToUpper();

        lettersToGuess = wordToGuess.ToCharArray();
        lettersGuessed = new bool[lengthOfWordToGuess];
    }

    void UpdateAttempts()
    {
        GameObject.Find("nbAttempts")
            .GetComponent<TextMeshProUGUI>().text =
            numberOfAttempts + "/" + maximumAttempts;
    }

    void UpdateScore()
    {
        GameObject.Find("scoreUI")
            .GetComponent<TextMeshProUGUI>().text =
            "Score: " + score;
    }


    void CheckAttemptLimit()
    {
        if (numberOfAttempts >= maximumAttempts)
        {
            SceneManager.LoadScene("wordGameEnd");
        }
    }

    void CheckIfWordWasFound()
    {
        bool allLettersGuessed = true;

        for (int i = 0; i < lengthOfWordToGuess; i++)
        {
            if (!lettersGuessed[i])
            {
                allLettersGuessed = false;
                break;
            }
        }

        if (allLettersGuessed)
        {
            PlayerPrefs.SetString("lastWordGuessed", wordToGuess);
            SceneManager.LoadScene("wordGameWin");
        }
    }
    void CheckKeyboard()
    {
        if (Input.inputString.Length > 0)
        {
            char letterPressed = Input.inputString[0];
            letterPressed = char.ToUpper(letterPressed);

            if (letterPressed >= 'A' && letterPressed <= 'Z')
            {
                numberOfAttempts++;
                UpdateAttempts();
                CheckAttemptLimit();

                for (int i = 0; i < lengthOfWordToGuess; i++)
                {
                    if (!lettersGuessed[i] &&
                        lettersToGuess[i] == letterPressed)
                    {
                        lettersGuessed[i] = true;

                        GameObject.Find("letter" + (i + 1))
                            .GetComponent<TextMeshProUGUI>().text =
                            letterPressed.ToString();

                        score = PlayerPrefs.GetInt("score");
                        score++;
                        PlayerPrefs.SetInt("score", score);
                        UpdateScore();
                        CheckIfWordWasFound();
                    }
                }
            }
        }
    }

    void InitLetters()
    {
        int numberOfLetters = lengthOfWordToGuess;

        for (int i = 0; i < numberOfLetters; i++)
        {
            Vector3 newPosition = new Vector3(
                center.transform.position.x + ((i - numberOfLetters / 2.0f) * 100),
                center.transform.position.y,
                center.transform.position.z
             );

            GameObject newLetter = Instantiate(
                letter,
                newPosition,
                Quaternion.identity
            );

            newLetter.name = "letter" + (i + 1);
            newLetter.transform.SetParent(
                GameObject.Find("Canvas").transform
            );
        }
    }
}