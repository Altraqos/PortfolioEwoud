using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour {

    public Slider MenuVolumeBar;
    public AudioSource MenuMusic;

    SaveScript saveScript;

    void Start()
    {
        SetInitialReferences();
        MenuMusic.volume = saveScript.MusicVolumeCurrent;
    }

    void SetInitialReferences()
    {
        saveScript = GetComponent<SaveScript>();
    }

    void Update()
    {
        if (MenuVolumeBar != null)
        {
            MenuMusic.volume = MenuVolumeBar.value;
            saveScript.MusicVolumeCurrent = (float)MenuVolumeBar.value;
        }
        else
        {
            return;
        }
    }
}
