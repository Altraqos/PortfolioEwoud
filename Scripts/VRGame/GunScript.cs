using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public ParticleSystem pSystem;
    public GameObject projectilePrefab;
    public Transform instantiatePoint;
    public float projectileSpeed = 15;
    public void Shoot()
    {
        pSystem.Play();

        GameObject projectileHolder;
        projectileHolder = Instantiate(projectilePrefab, instantiatePoint.position, this.gameObject.transform.rotation);
        projectileHolder.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
    }
}
