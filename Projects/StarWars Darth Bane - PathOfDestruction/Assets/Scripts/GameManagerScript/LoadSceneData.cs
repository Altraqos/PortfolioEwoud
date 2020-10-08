using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneData : MonoBehaviour {

	SaveScript data;

	void Awake () 
	{
		SetInitialReferences();
		data.LoadInGame();
	}

	void SetInitialReferences()
	{
		data = GetComponent<SaveScript>();
	}
}
