using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public AudioSource aSource;

    public void loadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ButtonSound(AudioClip audioClip)
    {
        aSource.clip = audioClip;
        aSource.Play();
    }
}
