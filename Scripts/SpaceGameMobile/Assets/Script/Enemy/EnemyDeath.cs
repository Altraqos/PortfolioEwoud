using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public int health;
    public ParticleSystem pSystem;
    public AudioSource aSource;
    public AudioClip aClip;

    public ShipManager sManager;
    int Value = 0;

    public void OnEnable()
    {
        sManager = GameObject.FindWithTag("Player").GetComponent<ShipManager>();
        Value = Random.Range(100, 200);
    }

    public void DeductHealth(int change)
    {
        health -= change;
        if (health <= 0)
        {
            sManager.IncreaseMoney(Value);
            health = 0;
            EnemyExplosion();
        }
    }

    public void EnemyExplosion()
    {
        aSource.clip = aClip;
        aSource.Play();
        pSystem.Play();
        Destroy(gameObject);
    }
}
