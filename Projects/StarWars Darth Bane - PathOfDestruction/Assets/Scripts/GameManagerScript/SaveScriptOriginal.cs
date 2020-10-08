using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveScriptOriginal : MonoBehaviour {

	public int StrenghtSkillCurrent;
	public int DefensiveSkillCurrent;
	public int SpeedSkillCurrent;
	public int StaminaCurrent;
	public int ManaCurrent;
	public int skillpointCurrent;
	public int FactionNumberCurrent;
	public int RaceNumberCurrent;
	public int WorldOpenCurrent;
    public float MenuMusicVolumeCurrent;

    public void Save() 
	{
        if (!Directory.Exists(Application.dataPath + "/Saves"))
            Directory.CreateDirectory(Application.dataPath + "/Saves");

        //string destination = Application.dataPath + "Saves/save.dat";
        //FileStream file;

        /*if (File.Exists (Application.dataPath + "Saves/save.dat")) 
		{
            FileStream file = File.OpenWrite(Application.dataPath + "Saves/save.dat");
		}
		else*/

        FileStream  file = File.Create(Application.dataPath + "save.dat");

		GameDataOriginal data = new GameDataOriginal(StrenghtSkillCurrent, DefensiveSkillCurrent, SpeedSkillCurrent, StaminaCurrent, ManaCurrent, skillpointCurrent, WorldOpenCurrent, MenuMusicVolumeCurrent);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);

		file.Close();
	}

	public void LoadInGame()
	{
		//Alle Data van de player Race, Faction, locatie, etc.
		string destination = Application.dataPath + "save.dat";
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
        MenuMusicVolumeCurrent = data.MusicVolumeSave;
    }

}
