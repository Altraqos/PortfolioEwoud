using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientScreenResolution : MonoBehaviour
{
    public int xVal;
    public int yVal;

    public void OnButtonClick()
    {
        AjustScreenResolution(xVal, yVal);
    }

    public void ToggleWindowed()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void AjustScreenResolution(int x, int y)
    {
        Screen.SetResolution(x, y, true);

        Debug.Log("Screen resolution is now: " + x + " by " + y);
    }
}
