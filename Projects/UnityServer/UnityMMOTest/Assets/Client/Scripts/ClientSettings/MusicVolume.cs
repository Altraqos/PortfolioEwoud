using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour 
{

	public Slider Volume;
	public AudioSource MenuMusic;

	void Update () 
	{
		MenuMusic.volume = Volume.value / 100;
	}
}
