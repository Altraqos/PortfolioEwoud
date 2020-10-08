using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public Canvas MainMenuCanvas;
    public Canvas OptionsCanvas;

    private void OnEnable()
    {
        MainMenuButton();
    }

    public void MainMenuButton()
    {
        MainMenuCanvas.enabled = true;
        OptionsCanvas.enabled = false;
    }

    public void Options()
    {
        MainMenuCanvas.enabled = false;
        OptionsCanvas.enabled = true;
    }

    public void LoadGame()
    {
        //eerst controleren of er al savegame is, zo niet niet deze gameobject actief zetten.
    }

    public void NewGame()
    {
        //nieuwe scene inladen voor char creation etc.
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
