using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningLight : MonoBehaviour
{

    public Light ThunderLight;
    public float ThunderIntensity = 2;
    public float[] thunderInterval = new float[1];

    void Start()
    {
        StartCoroutine("ThunderOn");
    }

    void Update()
    {

    }

    public IEnumerator ThunderOn()
    {
            while (ThunderLight.intensity <= ThunderIntensity);
            {
                ThunderLight.intensity -= Time.deltaTime;
                yield return null;
            }
        if (ThunderLight.intensity == 0)
        {
            StartCoroutine("ThunderOff");
            StopCoroutine("ThunderOn");
        }
    }

    public IEnumerator ThunderOff()
    {
            while (ThunderLight.intensity >= 0)
            {
                ThunderLight.intensity += Time.deltaTime;
                yield return null;
            }
        if (ThunderLight.intensity == ThunderIntensity)
        {
            StartCoroutine("ThunderOn");
            StopCoroutine("ThunderOff");
        }
    }
}
