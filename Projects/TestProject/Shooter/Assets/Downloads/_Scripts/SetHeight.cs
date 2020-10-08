using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHeight : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;

    Material material;

    private void OnEnable()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    void Update()
    {
        if (useBuffer && AudioMaster.audioBand[band] > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioMaster.audioBandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z);
            Color color = new Color(AudioMaster.audioBandBuffer[band], AudioMaster.audioBandBuffer[band], AudioMaster.audioBandBuffer[band]);
            material.SetColor("_EmissionColor", color);
        }
        if (!useBuffer && AudioMaster.audioBand[band] > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioMaster.audioBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
            Color color = new Color(AudioMaster.audioBand[band], AudioMaster.audioBand[band], AudioMaster.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }
    }
}
