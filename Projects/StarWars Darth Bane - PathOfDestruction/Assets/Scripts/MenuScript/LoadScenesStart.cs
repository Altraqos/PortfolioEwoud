using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesStart : MonoBehaviour {

	SaveScript data;

	void Start () 
	{
		SetInitialReferences();
	}

	public void LoadWorldSave()
	{
		SceneManager.LoadScene(data.WorldOpenCurrent);
	}

	public void StartGame()
	{
        SceneManager.LoadScene(1);
    }

	void SetInitialReferences()
	{
		data = GetComponent<SaveScript>();
	}
}
