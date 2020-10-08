using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public ParticleSystem pSystem;

    public void Shoot()
    {
        pSystem.Play();
        Debug.Log("Shooting Gun");
    }
}
