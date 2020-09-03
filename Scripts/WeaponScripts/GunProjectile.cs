using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    public float RotationSpeed = 5;
    public float destroyDelay = 5;
    public Vector3 rotationPoint;

    public void OnEnable()
    {
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
        transform.Rotate(rotationPoint * (RotationSpeed * Time.deltaTime));
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Debug.Log("Destroyed " + this.gameObject);
    }
}
