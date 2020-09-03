using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIFollowMouse : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
