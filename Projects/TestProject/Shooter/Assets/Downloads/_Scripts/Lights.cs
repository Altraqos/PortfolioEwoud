using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public int band;
    public bool useBuffer;
    public float minIntensity, maxIntensity;
    Light light;

    public float stuffs;

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;

    private void OnEnable()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        stuffs = AudioMaster.audioBandBuffer[band];
        light.intensity = (AudioMaster.audioBandBuffer[band] * (maxIntensity - minIntensity)) + minIntensity / 2;
        if (light.intensity <= 4f)
        {
            light.color = color1;
        }
        if (light.intensity > 4f && AudioMaster.audioBandBuffer[band] < 6f)
        {
            light.color = color2;
        }
        if (light.intensity > 6f && AudioMaster.audioBandBuffer[band] < 8f)
        {
            light.color = color3;
        }
        else
        {
            light.color = color4;
        }
    }
}
