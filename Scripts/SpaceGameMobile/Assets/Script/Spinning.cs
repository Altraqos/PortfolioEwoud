using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float RotateX = 20;
    public float RotateY = 20;
    public float RotateZ = 20;

    public float Speed = 10;

    void Update()
    {
        transform.Rotate(new Vector3(RotateX, RotateY, RotateZ), Speed / 10);
    }
}
