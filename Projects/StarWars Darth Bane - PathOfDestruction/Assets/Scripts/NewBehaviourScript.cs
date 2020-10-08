using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    SaveScript saveScript;

	void Start ()
    {
        saveScript = GetComponent<SaveScript>();
        SetWorld();
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetWorld()
    {
        saveScript.WorldOpenCurrent = 3;
        saveScript.Save();
    }
}
