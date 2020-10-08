using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardConnectionID : MonoBehaviour
{
    public int ConnectionIDHolder;

    public void GetConnectionID(int ID)
    {
        DontDestroyOnLoad(this);
        ConnectionIDHolder = ID;
    }
}
