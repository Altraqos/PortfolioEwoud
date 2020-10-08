using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{
    PlayerMaster pMaster;
    Image[] NumberPosArray;
    public Image NumberPosHolder;

    private void OnEnable()
    {
        pMaster = GetComponent<PlayerMaster>();
        NumberPosArray = new Image[pMaster.maxScore.ToString().Length];
        for (int i = 0; i < pMaster.maxScore.ToString().Length; i++)
            NumberPosArray[i] = Instantiate(NumberPosHolder, new Vector3((NumberPosHolder.sprite.rect.width * i) + NumberPosHolder.transform.position.x, NumberPosHolder.transform.position.y, NumberPosHolder.transform.position.z), Quaternion.identity, pMaster.ScoreCanvas.transform) as Image;
    }

    public void IncreasePoints(int amount)
    {
        if (pMaster.CurrentScore + amount >= 0)
            pMaster.CurrentScore += amount;
        if (pMaster.CurrentScore + amount > pMaster.maxScore)
            pMaster.CurrentScore = pMaster.maxScore;
        LoadScoreSprites();
    }

    private void LoadScoreSprites()
    {
        for (int i = 0; i < pMaster.maxScore.ToString().Length; i++)
            NumberPosArray[i].sprite = pMaster.Numbers[int.Parse(pMaster.CurrentScore.ToString().PadLeft(pMaster.maxScore.ToString().Length, '0')[i].ToString())];
    }
}