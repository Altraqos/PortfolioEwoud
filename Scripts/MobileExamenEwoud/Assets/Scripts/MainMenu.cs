using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas modeCanvas;
    public Canvas chooseLevel;
    public Canvas gameMenuCanvas;
    public Canvas scoreCanvas;
    public Text ChosenDifficulty;
    public Text ChosenMode;
    public Text VictoryText;
    public Text HighScoreText;

    public void OnEnable()
    {
        gameObject.AddComponent<CardManager>();
        GameManager.instance.cManager = GetComponent<CardManager>();
        GameManager.instance.cManager.enabled = false;
        modeCanvas.enabled = false;
        menuCanvas.enabled = true;
        chooseLevel.enabled = false;
        gameMenuCanvas.enabled = false;
        scoreCanvas.enabled = false;
        GameManager.instance.playerCanvas.enabled = false;
        ChosenDifficulty.text = "Choose your level!";
        ChosenMode.text = "Choose your mode!";
        VictoryText.text = null;
        VictoryText.enabled = false;
        ToggleMode(true);
    }

    public void ChooseMode()
    {
        GameManager.instance.ShowHighScores();
        VictoryText.enabled = true;
        ChosenMode.text = "Choose your mode!";
        GameManager.instance.cManager.enabled = false;
        modeCanvas.enabled = true;
        menuCanvas.enabled = false;
        chooseLevel.enabled = false;
        scoreCanvas.enabled = false;
        GameManager.instance.timePlayed = 0;
        GameManager.instance.playerScore = 0;
        GameManager.instance.comboCount = 0;
    }

    public void OpenMenu()
    {
        GameManager.instance.inGameMenuOpen = !GameManager.instance.inGameMenuOpen;
        if (GameManager.instance.inGameMenuOpen)
        {
            gameMenuCanvas.enabled = true;
            GameManager.instance.isPlaying = false;
        }
        if (!GameManager.instance.inGameMenuOpen)
        {
            gameMenuCanvas.enabled = false;
            GameManager.instance.isPlaying = true;
        }
    }

    public void ChooseLevelButton(int Level)
    {
        if (GameManager.instance.isClassic)
            GameManager.instance.currentLevelClassicPlaying = Level;
        if (GameManager.instance.isShuffle)
            GameManager.instance.currentLevelShufflePlaying = Level;
        ChosenDifficulty.text = "You have chosen level" + Level + ", you will play with " + Level * 4 + " cards.";
        GameManager.instance.cardsToPlay = Level * 4;
    }

    public void ChooseLevel()
    {
        scoreCanvas.enabled = false;
        if (GameManager.instance.isShuffle)
        {
            ChosenDifficulty.text = "You have chosen level" + GameManager.instance.currentLevelShuffle + ", you will play with " + GameManager.instance.currentLevelShuffle * 4 + " cards.";
            GameManager.instance.UnlockLevel(GameManager.instance.currentLevelShuffle);
            ChooseLevelButton(GameManager.instance.currentLevelShuffle);
        }
        if (GameManager.instance.isClassic)
        {
            ChosenDifficulty.text = "You have chosen level" + GameManager.instance.currentLevelClassic + ", you will play with " + GameManager.instance.currentLevelClassic * 4 + " cards.";
            GameManager.instance.UnlockLevel(GameManager.instance.currentLevelClassic);
            ChooseLevelButton(GameManager.instance.currentLevelClassic);
        }
        GameManager.instance.playerCanvas.enabled = false;
        modeCanvas.enabled = false;
        menuCanvas.enabled = false;
        chooseLevel.enabled = true;
    }

    public void StartGame()
    {
        GameManager.instance.inGameMenuOpen = false;
        scoreCanvas.enabled = false;
        modeCanvas.enabled = false;
        menuCanvas.enabled = false;
        chooseLevel.enabled = false;
        GameManager.instance.cManager.enabled = true;
        GameManager.instance.playerCanvas.enabled = true;
        scoreCanvas.enabled = true;
        if (GameManager.instance.isShuffle)
            GameManager.instance.StartCoroutine("ShuffleInGame");
        GameManager.instance.isPlaying = true;
        GameManager.instance.StartCoroutine("Timer");
    }

    public void Back()
    {
        GameManager.instance.DestroyCards();
        modeCanvas.enabled = false;
        menuCanvas.enabled = true;
        chooseLevel.enabled = false;
        scoreCanvas.enabled = false;
        gameMenuCanvas.enabled = false;
        GameManager.instance.playerCanvas.enabled = false;
        gameMenuCanvas.enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToggleMode(bool toggle)
    {
        if (toggle)
        {
            ChosenMode.text = "Chosen for classic mode!";
            GameManager.instance.isClassic = true;
            GameManager.instance.isShuffle = false;
            GameManager.instance.ShowHighScores();
        }
        if (!toggle)
        {
            ChosenMode.text = "Chosen for shuffle mode!";
            GameManager.instance.isClassic = false;
            GameManager.instance.isShuffle = true;
            GameManager.instance.ShowHighScores();
        }
    }
}