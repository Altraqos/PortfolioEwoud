using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour 
{
	public Canvas MainMenu;
    public Canvas SettingsMenu;

	void Start()
	{
        MainMenu.enabled = true;
        SettingsMenu.enabled = false;
    }


    public void OpenSettingsMenu()
    {
        MainMenu.enabled = false;
        SettingsMenu.enabled = true;
    }

    public void CloseSettingsMenu()
    {
        MainMenu.enabled = true;
        SettingsMenu.enabled = false;
    }

    public void ExitGame()
	{
		Application.Quit();
	}
}
