using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentLevelClassic = 1;
    public int currentLevelShuffle = 1;
    public float offSet = 60;
    public float heightOffSet = 100;
    public float shuffleTime = 5;
    public float delayTime = 3f;
    float currentTime;
    public Canvas playerCanvas;
    public Text playerScoreText;
    public Text playerTimeText;
    public Text comboText;
    public Text shuffleText;
    public GameObject cardPref;
    public GameObject playerCam;
    public Transform buttonListHolder;
    public Image ImagePos;
    public List<Color> cardColors;
    public Dictionary<int, Color> cardValues;
    public Dictionary<int, string> TimeRecordClassic;
    public Dictionary<int, int> ScoreRecordClassic;    
    public Dictionary<int, string> TimeRecordShuffle;
    public Dictionary<int, int> ScoreRecordShuffle;
    [HideInInspector] public List<Button> levelSelect;
    [HideInInspector] public CardManager cManager;
    [HideInInspector] public Vector3 cameraPosOriginal;
    [HideInInspector] public MainMenu mMenu;
    [HideInInspector] public Card firstChoice;
    [HideInInspector] public Card secondChoice;
    [HideInInspector] public int currentLevelClassicPlaying = 1;
    [HideInInspector] public int currentLevelShufflePlaying = 1;
    [HideInInspector] public int cardsToPlay = 0;
    [HideInInspector] public int playerScore = 0;
    [HideInInspector] public int timePlayed = 0;
    [HideInInspector] public int comboCount = 0;
    [HideInInspector] public int valSeconds = 0;
    [HideInInspector] public int valMinutes = 0;
    [HideInInspector] public bool isShuffle = false;
    [HideInInspector] public bool isClassic = false;
    [HideInInspector] public bool isChoosing = false;
    [HideInInspector] public bool isPlaying = false;
    [HideInInspector] bool failSave = false;
    [HideInInspector] public bool inGameMenuOpen = false;

    public void Awake()
    {
        cameraPosOriginal = playerCam.transform.position;
        ScoreRecordClassic = new Dictionary<int, int>();
        TimeRecordClassic = new Dictionary<int, string>();
        ScoreRecordShuffle = new Dictionary<int, int>();
        TimeRecordShuffle = new Dictionary<int, string>();
        levelSelect = new List<Button>();
        shuffleText.enabled = false;
        mMenu = GetComponent<MainMenu>();
        instance = this;
        AssignValue();
        playerScoreText.text = "Score of: " + playerScore.ToString();
        comboText.text = "Combo of: " + comboCount.ToString() + "!";
        foreach (Transform item in buttonListHolder)
            levelSelect.Add(item.transform.GetComponent<Button>()) ;
        for (int i = 0; i < levelSelect.Count; i++)
        {
            ScoreRecordClassic.Add(i, 0);
            TimeRecordClassic.Add(i, "00:00");        
            ScoreRecordShuffle.Add(i, 0);
            TimeRecordShuffle.Add(i, "00:00");
        }  
    }

    public void UnlockLevel(int levelsUnlocked)
    {
        for (int i = 0; i < levelSelect.Count; i++)
            levelSelect[i].interactable = true;
        for (int i = levelsUnlocked; i < levelSelect.Count; i++)
            levelSelect[i].interactable = false;
    }

    public IEnumerator CompareCards()
    {
        if (firstChoice.cardVal == secondChoice.cardVal)
        {
            comboCount++;
            playerScore += 10 * comboCount;
            firstChoice.toEnd = true;
            secondChoice.toEnd = true;
            firstChoice = null;
            secondChoice = null;
            isChoosing = false;
        }
        else if (firstChoice.cardVal != secondChoice.cardVal && secondChoice != null)
        {
            failSave = true;
            comboCount = 0;
            playerScore -= Random.Range(1, 3);
            yield return new WaitForSecondsRealtime(0.5f);
            firstChoice.FlipCard(false);
            secondChoice.FlipCard(false); 
            firstChoice.isFlipping = false;
            secondChoice.isFlipping = false;
            firstChoice.isClicked = false;
            secondChoice.isClicked = false;
            firstChoice = null;
            secondChoice = null;
            isChoosing = false;
            failSave = false;
        }
        playerScoreText.text = "Score of: " + playerScore.ToString();
        comboText.text = "Combo of: " + comboCount.ToString() + "!";
    }

    public void ShowHighScores()
    {
        if (isShuffle)
        {
            if (ScoreRecordShuffle[currentLevelShufflePlaying - 1] < playerScore)
            {
                ScoreRecordShuffle[currentLevelShufflePlaying - 1] = playerScore;
                TimeRecordShuffle[currentLevelShufflePlaying - 1] = valMinutes.ToString("00") + ":" + valSeconds.ToString("00");
            }
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            sBuilder.Append("HighScores Shuffle: " + "\n");
            foreach (var item in ScoreRecordShuffle)
            {
                if(item.Value != 0)
                sBuilder.Append("Level " + (item.Key + 1).ToString("00") + ": " + item.Value.ToString("0000") +  " | " + TimeRecordShuffle[item.Key] + "\n");
            }
            mMenu.HighScoreText.text = sBuilder.ToString();
        }
        if (isClassic)
        {
            if (ScoreRecordClassic[currentLevelClassicPlaying - 1] < playerScore)
            {
                ScoreRecordClassic[currentLevelClassicPlaying - 1] = playerScore;
                TimeRecordClassic[currentLevelClassicPlaying - 1] = valMinutes.ToString("00") + ":" + valSeconds.ToString("00");
            }
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            sBuilder.Append("HighScores Classic: " + "\n");
            foreach (var item in ScoreRecordClassic)
            {
                if (item.Value != 0)
                    sBuilder.Append("Level " + (item.Key + 1).ToString("00") + ": " + item.Value.ToString("0000") + " | " + TimeRecordClassic[item.Key] + "\n");
            }
            mMenu.HighScoreText.text = sBuilder.ToString();
        }
    }

    public void DestroyCards()
    {
        playerScoreText.text = "Score of: 0";
        comboText.text = "Combo of: 0!";
        foreach (var card in cManager.cardHolder)
            Destroy(card.gameObject);
        cManager.cardHolder.Clear();
        StopAllCoroutines();
    }

    public IEnumerator ShuffleInGame()
    {
        shuffleText.enabled = true;
        currentTime = shuffleTime;
        while (true)
        {
            while (!isPlaying || failSave)
                yield return null;
            shuffleText.text = currentTime.ToString("00.00");
            yield return new WaitForSecondsRealtime(0.1f);
            currentTime -= 0.1f;
            if (currentTime < 0.1f)
            {
                StopCoroutine("CompareCards");
                if (firstChoice != null)
                {
                    firstChoice.isFlipping = false;
                    firstChoice.isClicked = false;
                }
                if (secondChoice != null)
                {
                    secondChoice.isFlipping = false;
                    secondChoice.isClicked = false;
                }
                firstChoice = null;
                secondChoice = null;
                isChoosing = false;
                for (int i = 0; i < cManager.cardList.Count; i++)
                    cManager.Shuffle(cManager.cardColor, i, Random.Range(i, cManager.cardList.Count));
                currentTime = shuffleTime;
            }
        }
    }

    public IEnumerator Timer()
    {
        valSeconds = 0;
        valMinutes = 0;
        playerTimeText.text = "Time played: " + valMinutes.ToString("00") + " : " + valSeconds.ToString("00");
        while (true)
        {
            while (!isPlaying)
                yield return null;
            yield return new WaitForSecondsRealtime(1f);
            valSeconds++;
            playerTimeText.text = "Time played: " + valMinutes.ToString("00") + " : " + valSeconds.ToString("00");
            if (valSeconds > 59)
            {
                valSeconds = 0;
                valMinutes++;
            }
        }
    }

    public void AssignValue()
    {
        cardValues = new Dictionary<int, Color>();
        for (int i = 0; i < cardColors.Count; i++)
            cardValues.Add(i, cardColors[i]);
    }
}