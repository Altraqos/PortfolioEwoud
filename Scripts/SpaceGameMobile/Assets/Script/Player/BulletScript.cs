using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject playerShip;
    public int Damage = 200;
    void OnEnable()
    {
        playerShip = GameObject.FindWithTag("Player");
        Rigidbody rBody = gameObject.GetComponent<Rigidbody>();
        //rBody.velocity = new Vector3(0, 0, 15);
        rBody.velocity = playerShip.transform.forward * 15;
        Destroy(gameObject, 6f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyDeath eDeath = other.gameObject.GetComponent<EnemyDeath>();
            if(eDeath != null)
            eDeath.DeductHealth(Damage);
            Destroy(gameObject);
        }
    }
}
