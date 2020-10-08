using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    //TestVariabelen voor UI te testen
    public int ExpToAdd = 50000;
    public int ScoreToAdd = 333;
    public PlayerScoreManager pScoreManager;
    public PlayerHealth pHealth;
    public PlayerLevel pLevel;
    //________________________________//
    public Canvas ScoreCanvas;
    public Canvas LevelCanvas;
    public Sprite[] Numbers;

    public float CanvasDelay = 1.5f;

    public int CurrentScore;
    public int maxScore = 99999999;

    public int CurrentHealth;
    public int MaxHealth = 100;
    public int BaseHealth = 100;

    public int CurrentExp;
    public int ExpRequired = 850;
    public int BaseExp;
    public int expBonus = 2;

    public int CurrentLevel = 1;
    public int MaxLevel = 100;

    public int CurrentStamina;
    public int MaxStamina = 100;
    public int BaseStamina = 100;

    public void TestHealth()
    {
        pHealth.DeductHealth(15);
    }

    public void TestScore()
    {
        pScoreManager.IncreasePoints(ScoreToAdd);
    }

    public void TestExp()
    {
        pLevel.AddExp(ExpToAdd);
    }
}
