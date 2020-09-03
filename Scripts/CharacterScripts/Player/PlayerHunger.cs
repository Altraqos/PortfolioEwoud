using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunger : MonoBehaviour
{
    PlayerMaster pMaster;
    PlayerHealth pHealth;
    public float damageSpeed = 1;
    public int damageHealth = 1;

    public void Start()
    {
        pMaster = GetComponent<PlayerMaster>();
        pHealth = GetComponent<PlayerHealth>();
    }

    public void IncreaseHunger(int change)
    {
        if (pMaster.hunger != pMaster.maxHunger)
        {
            pMaster.hunger += change;
            if (pMaster.hunger + change > pMaster.maxHunger)
                pMaster.hunger = pMaster.maxHunger;
        }
    }

    public void DeductHunger(int change)
    {
        if (pMaster.hunger != 0)
        {
            pMaster.hunger -= change;
            if (pMaster.hunger + change < 0)
                pMaster.hunger = 0;
        }
        if (pMaster.hunger == 0)
            StartCoroutine("hungerDamage");
    }

    public IEnumerator hungerDamage()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(damageSpeed);
            pHealth.DeductHealth(damageHealth);
        }
    }
}
