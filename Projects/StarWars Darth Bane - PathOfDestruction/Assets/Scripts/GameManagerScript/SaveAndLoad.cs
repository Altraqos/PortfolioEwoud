using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour {

	public List<int> list1 = new List<int>();

	public savedata data;

	public Vector3 xyz = new Vector3();
	public 

	// Use this for initialization
	void Start () 
	{
		Load ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.O))
		{
			StartCoroutine("SystemSave");
		}

		if (Input.GetKeyDown (KeyCode.L)) 
		{
			Load();
		}
	}

	public void Save()
	{
		if (!Directory.Exists (Application.dataPath + "/Saves"))
			Directory.CreateDirectory (Application.dataPath + "/Saves");

		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create (Application.dataPath + "/Saves/SaveData.dat");

		CopySaveData ();

		bf.Serialize (file, data);
		file.Close ();
	}

	public void CopySaveData()
	{
		data.list1.Clear();

		foreach(int i in list1)
		{
			data.list1.Add(1);
		}

		data.position = Vector3ToSerVector3 (xyz);
	}

	public void Load()
	{
		if(File.Exists(Application.dataPath + "/Saves/SaveData.dat"))
			{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.dataPath + "/Saves/SaveData.dat", FileMode.Open);
			data = (savedata)bf.Deserialize(file);

			CopyLoadData ();

			file.Close ();
			}
	}

	public void CopyLoadData ()
	{
		list1.Clear ();
		foreach (int i in data.list1) 
		{
			list1.Add (i);
		}

		xyz = SerVector3ToVector (data.position);
	}

	public static SerVector3 Vector3ToSerVector3(Vector3 V3)
	{
		SerVector3 SV3 = new SerVector3 ();

		SV3.x = V3.x;
		SV3.y = V3.y;
		SV3.z = V3.z;

		return SV3;
	}

	public static Vector3 SerVector3ToVector(SerVector3 SV3)
	{
		Vector3 V3 = new Vector3 ();

		V3.x = SV3.x;
		V3.y = SV3.y;
		V3.z = SV3.z;

		return V3;
	}

	public IEnumerator SystemSave()
	{
		Time.timeScale = 0.1f;
		Save ();
		yield return new WaitForSeconds(0.01f);
		Time.timeScale = 1f;
	}
}

[System.Serializable]
public class savedata
{
	public SerVector3 position;
	public List<int> list1 = new List<int>();
}

[System.Serializable]
public class SerVector3
{
	public float x;
	public float y;
	public float z;
}