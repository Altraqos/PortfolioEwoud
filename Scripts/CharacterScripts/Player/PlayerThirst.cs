using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirst : MonoBehaviour 
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
        if (pMaster.thirst != pMaster.maxThirst)
        {
            pMaster.thirst += change;
            if (pMaster.thirst + change > pMaster.maxThirst)
                pMaster.thirst = pMaster.maxThirst;
        }
    }

    public void DeductHunger(int change)
    {
        if (pMaster.thirst != 0)
        {
            pMaster.thirst -= change;
            if (pMaster.thirst + change < 0)
                pMaster.thirst = 0;
        }
        if (pMaster.hunger == 0)
            StartCoroutine("thirstDamage");
    }

    public IEnumerator thirstDamage()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(damageSpeed);
            pHealth.DeductHealth(damageHealth);
        }
    }
}
