[System.Serializable]
public class GameData
{
	public int StrenghtSkillSave;
	public int DefensiveSkillSave;
	public int SpeedSkillSave;
	public int StaminaSkillSave;
	public int ManaSkillSave;
	public int SkillPointsSave;
	public int WorldOpenSave;
    public float MusicVolumeSave;

	public GameData(int StrenghtSkill, int DefensiveSkill, int SpeedSkill, int StaminaSkill, int ManaSkill, int SkillPointsVal, int WorldOpenVal, float MusicVolume)
	{
		StrenghtSkillSave = StrenghtSkill;
		DefensiveSkillSave = DefensiveSkill;
		SpeedSkillSave = SpeedSkill;
		StaminaSkillSave = StaminaSkill;
		ManaSkillSave = ManaSkill;
		SkillPointsSave = SkillPointsVal;
		WorldOpenSave = WorldOpenVal;
        MusicVolumeSave = MusicVolume;
	}

}
