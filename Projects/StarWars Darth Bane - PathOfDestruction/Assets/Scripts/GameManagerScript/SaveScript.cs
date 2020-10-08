using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour {

	public int StrenghtSkillCurrent;
	public int DefensiveSkillCurrent;
	public int SpeedSkillCurrent;
	public int StaminaCurrent;
	public int ManaCurrent;
	public int skillpointCurrent;
	public int WorldOpenCurrent;

    public float MusicVolumeCurrent;

    public void Start()
    {
        LoadInGame();
    }

        public void Save() 
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists (destination)) 
		{
			file = File.OpenWrite (destination);
		}
		else 
			file = File.Create(destination);

		GameData data = new GameData (StrenghtSkillCurrent, DefensiveSkillCurrent, SpeedSkillCurrent, StaminaCurrent, ManaCurrent, skillpointCurrent, WorldOpenCurrent, MusicVolumeCurrent);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);

		file.Close();
	}

	public void LoadInGame()
	{
		//Alle Data van de player Race, Faction, locatie, etc.
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists (destination)) 
		{
			file = File.OpenRead (destination);
		} 
		else 
		{
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		GameData data = (GameData)
		bf.Deserialize (file);
		file.Close();

		StrenghtSkillCurrent = data.StrenghtSkillSave;
		DefensiveSkillCurrent = data.DefensiveSkillSave;
		SpeedSkillCurrent = data.SpeedSkillSave;
		StaminaCurrent = data.StaminaSkillSave;
		ManaCurrent = data.ManaSkillSave;
		skillpointCurrent = data.SkillPointsSave;
		WorldOpenCurrent = data.WorldOpenSave;
        MusicVolumeCurrent = data.MusicVolumeSave;
    }
}
