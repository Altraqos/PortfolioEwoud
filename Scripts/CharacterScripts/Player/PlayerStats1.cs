using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats1 : MonoBehaviour {

	public float EXP = 0;
	public float EXPRequired = 825;
	public float CurrentLevel = 1;
    public float PlayerCurrentHealth = 100;
    public float CurrentMaxHealth = 100;
    public float PlayerBaseDamage = 10;
    public float MaxLevel = 80;
    public Text HealthText;
    public Text CurrentLevelText;

	private AudioSource source;
	public float speed = 10.0F;

    public Slider HealthBar;
    public Slider EXPBar;

    void Start()
    {
        PlayerCurrentHealth = CurrentMaxHealth; 
    }

    void Update () 
	{
		SetUIStats();
        if (EXP > EXPRequired) 
		{
			LevelUp();
		}
        if (CurrentLevel >= MaxLevel && EXP >= 0)
        {
            CurrentLevel = MaxLevel;
            EXP = 0;
        }
        if (PlayerCurrentHealth >= CurrentMaxHealth)
        {
            PlayerCurrentHealth = CurrentMaxHealth;
        }
    }

	void LevelUp()
	{
        EXP -= EXPRequired;
		CurrentLevel += 1;
        CurrentMaxHealth += 15 * CurrentLevel;
        PlayerBaseDamage += 5 * CurrentLevel;
		EXPRequired += 350 * CurrentLevel;
        PlayerCurrentHealth = CurrentMaxHealth;
    }

	public void SetUIStats()
	{
            CurrentLevelText.text = CurrentLevel.ToString();
            HealthText.text = Mathf.RoundToInt(PlayerCurrentHealth).ToString();
            HealthBar.maxValue = CurrentMaxHealth;
            HealthBar.value = PlayerCurrentHealth;
            EXPBar.maxValue = EXPRequired;
            EXPBar.value = EXP;
    }
}

