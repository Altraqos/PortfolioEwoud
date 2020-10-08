using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas OptionCanvas;
    public Canvas CreditCanvas;

    public static string ApplicationPath;
    public static string ClientDataPath;
    public static string ClientDataFilePath;

    public Text DevText;

    public bool isFirstTime = true;

    public void OnEnable()
    {
        ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
        ClientDataPath = ApplicationPath + @"\Data";
        ClientDataFilePath = ClientDataPath + @"\Settings.wtf";

        if (isFirstTime)
        {
            FirstTimeStart();
        }
        ReturnToMainMenu();
    }

    public void GoToOptions()
    {
        MainCanvas.enabled = false;
        OptionCanvas.enabled = true;
        CreditCanvas.enabled = false;
    }

    public void GoToCredits()
    {
        MainCanvas.enabled = false;
        OptionCanvas.enabled = false;
        CreditCanvas.enabled = true;
    }

    public void ReturnToMainMenu()
    {
        MainCanvas.enabled = true;
        OptionCanvas.enabled = false;
        CreditCanvas.enabled = false;
    }

    public void QuitGame()
    {
        MainCanvas.enabled = false;
        OptionCanvas.enabled = false;
        CreditCanvas.enabled = false;
        Environment.Exit(0);
    }

    public void FirstTimeStart()
    {
        if (!Directory.Exists(ClientDataPath))
        {
            try
            {
                Directory.CreateDirectory(ClientDataPath);
                Debug.Log("Couldn't find a 'UserData' folder at " + ApplicationPath + ", created the folder.");
                DevText.text = "Couldn't find a 'UserData' folder at " + ApplicationPath + ", created the folder.";
            }
            catch
            {
                Debug.Log("Couldn't create a 'UserData' folder at " + ApplicationPath + ", please try again.");
                DevText.text = "Couldn't create a 'UserData' folder at " + ApplicationPath + ", please try again.";
            }
        }

        if (!File.Exists(ClientDataFilePath))
        {
            try
            {
                FileStream fs = File.Open(ClientDataFilePath, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(fs);

                fw.WriteLine("1920 x 1080");
                fw.Flush();
                fw.Close();
            }
            catch
            {
                DevText.text = "Error...";
            }
        }
    }
}
