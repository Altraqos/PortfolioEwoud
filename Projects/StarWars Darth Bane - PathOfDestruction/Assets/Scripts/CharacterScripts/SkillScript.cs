using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour {

	public Text StrenghtText;
	public Text DefenceText;
	public Text SpeedText;
	public Text StaminaText;
	public Text ManaText;
	public Text SkillPointsLeft;

	SaveScript data;

	public void StrenghtSkillIncrease()
	{
		if (data.skillpointCurrent > 0) 
		{
			data.skillpointCurrent--;
			data.StrenghtSkillCurrent++;
		}
	}

	public void StrenghtSkillDecrease()
	{
		if (data.StrenghtSkillCurrent > 5) 
		{
			data.StrenghtSkillCurrent--;
			data.skillpointCurrent++;
		}
	}
		
	public void DefenceSkillIncrease()
	{
		if (data.skillpointCurrent > 0) 
		{
			data.skillpointCurrent--;
			data.DefensiveSkillCurrent++;
		}
	}

	public void DefenceSkillDecrease()
	{
		if (data.DefensiveSkillCurrent > 5) 
		{
			data.DefensiveSkillCurrent--;
			data.skillpointCurrent++;
		}
	}

	public void SpeedSkillIncrease()
	{
		if (data.skillpointCurrent > 0) 
		{
			data.skillpointCurrent--;
			data.SpeedSkillCurrent++;
		}
	}

	public void SpeedSkillDecrease()
	{
		if (data.SpeedSkillCurrent > 5) 
		{
			data.SpeedSkillCurrent--;
			data.skillpointCurrent++;
		}
	}

	public void StaminaSkillIncrease()
	{
		if (data.skillpointCurrent > 0) 
		{
			data.skillpointCurrent--;
			data.StaminaCurrent++;
		}
	}

	public void StaminaSkillDecrease()
	{
		if (data.StaminaCurrent > 5) 
		{
			data.StaminaCurrent--;
			data.skillpointCurrent++;
		}
	}

	public void ManaSkillDecrease()
	{
		if (data.ManaCurrent > 5) 
		{
			data.ManaCurrent--;
			data.skillpointCurrent++;
		}
	}

	public void ManaSkillIncrease()
	{
		if (data.skillpointCurrent > 0) 
		{
			data.skillpointCurrent--;
			data.ManaCurrent++;
		}
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences() 
	{
		data = GetComponent<SaveScript>();
	}

	void Update () 
	{
		SetUI();
	}

	void SetUI()
	{
		if (StrenghtText != null) 
		{
			StrenghtText.text = "Strenght: " + data.StrenghtSkillCurrent.ToString ();
		}

		if (DefenceText != null) 
		{
			DefenceText.text = "Defence: " + data.DefensiveSkillCurrent.ToString ();
		}

		if (SpeedText != null) 
		{
			SpeedText.text = "Speed: " + data.SpeedSkillCurrent.ToString ();
		}

		if (StaminaText != null) 
		{
			StaminaText.text = "Stamina: " + data.StaminaCurrent.ToString ();
		}

		if (ManaText != null) 
		{
			ManaText.text = "Mana: " + data.ManaCurrent.ToString ();
		}

		if (SkillPointsLeft != null) 
		{
			SkillPointsLeft.text = "Skillpoints Left: " + data.skillpointCurrent.ToString ();
		}
	}
}
