using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Color> cardColor;
    public List<Card> cardList;
    public List<Card> cardHolder;

    public void OnEnable()
    {
        cardList = new List<Card>();
        cardColor = new List<Color>();
        cardHolder = new List<Card>();
        GameManager.instance.isPlaying = true;
        GameManager.instance.playerCam.transform.position = new Vector3(-1.5f, GameManager.instance.cameraPosOriginal.y + (Mathf.Sqrt(GameManager.instance.cardsToPlay) / 4), GameManager.instance.cameraPosOriginal.z + (-Mathf.Sqrt(GameManager.instance.cardsToPlay) / 2));
        for (int i = 0; i < Mathf.Sqrt(GameManager.instance.cardsToPlay); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(GameManager.instance.cardsToPlay); j++)
            {
                var card = Instantiate(GameManager.instance.cardPref, new Vector2(((GameManager.instance.ImagePos.gameObject.transform.localScale.x * i / 10000000)) + GameManager.instance.ImagePos.transform.position.x + (GameManager.instance.offSet * i / 100), GameManager.instance.ImagePos.transform.position.y + (GameManager.instance.heightOffSet * j / 100)), Quaternion.identity) as GameObject;
                card.name = "Card: " + i + " - " + j;
                Card sCard = card.GetComponent<Card>();
                cardList.Add(sCard);
                cardHolder.Add(sCard);
            }
        }
        for (int i = 0; i < GameManager.instance.cardsToPlay / 2; i++)
        {
            for (int j = 0; j < 2; j++)
                cardColor.Add(GameManager.instance.cardColors[i]);
        }
        while (cardList.Count > cardColor.Count)
        {
            Destroy(cardList[cardList.Count - 1].gameObject);
            cardList.RemoveAt(cardList.Count - 1);
            cardHolder.RemoveAt(cardList.Count);
        }
        for (int i = 0; i < cardList.Count; i++)
            Shuffle(cardColor, i, Random.Range(i, cardList.Count));
    }

    public void RemoveMatch(int matchVal)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            if (cardList[i].cardVal == matchVal)
            {
                cardColor.RemoveAt(i);
                cardList.RemoveAt(i);
            }
            if (cardList.Count < 2)
            {
                cardList.Clear();
                cardColor.Clear();
                GameManager.instance.isPlaying = false;
                Invoke("VictoryDelay", GameManager.instance.delayTime);
            }
        }
    }

    void VictoryDelay()
    {
        GameManager.instance.mMenu.gameMenuCanvas.enabled = false;
        GameManager.instance.shuffleText.enabled = false;
        GameManager.instance.playerCanvas.enabled = false;
        GameManager.instance.mMenu.VictoryText.text = "You finished playing in: " + GameManager.instance.valMinutes.ToString("00") + " : " + GameManager.instance.valSeconds.ToString("00") + "!" + "\n" + "With a score of: " + GameManager.instance.playerScore + "\n" + "And a final combo of: " + GameManager.instance.comboCount;
        if (GameManager.instance.isShuffle && GameManager.instance.currentLevelShufflePlaying == GameManager.instance.currentLevelShuffle)
        {
            GameManager.instance.currentLevelShuffle = GameManager.instance.currentLevelShuffle + 1;
            GameManager.instance.UnlockLevel(GameManager.instance.currentLevelShuffle);
        }
        if (GameManager.instance.isClassic && GameManager.instance.currentLevelClassicPlaying == GameManager.instance.currentLevelClassic)
        {
            GameManager.instance.currentLevelClassic = GameManager.instance.currentLevelClassic + 1;
            GameManager.instance.UnlockLevel(GameManager.instance.currentLevelClassic);
        }
        GameManager.instance.ShowHighScores();
        GameManager.instance.cardValues.Clear();
        GameManager.instance.cardsToPlay = 0;
        GameManager.instance.playerScore = 0;
        GameManager.instance.timePlayed = 0;
        GameManager.instance.comboCount = 0;
        GameManager.instance.cardsToPlay = 0;
        GameManager.instance.valSeconds = 0;
        GameManager.instance.valMinutes = 0;
        GameManager.instance.StopAllCoroutines();
        GameManager.instance.DestroyCards();
        GameManager.instance.mMenu.ChooseMode();
    }

    public void Shuffle<T>(List<T> CardList, int i, int j)
    {
        var temp = cardList[i];
        cardList[i] = cardList[j];
        cardList[j] = temp;
        for (int x = 0; x < cardList.Count; x++)
            cardList[x].GetComponent<Card>().AssignValues(cardColor[x]);
    }
}