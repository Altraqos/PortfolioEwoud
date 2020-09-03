using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerMaster pMaster;

    public void Start()
    {
        pMaster = GetComponent<PlayerMaster>();
    }

    public void IncreaseHealth(int change)
    {
        if (pMaster.health != pMaster.maxHealth)
        {
            pMaster.health += change;
            if (pMaster.health + change > pMaster.maxHealth)
                pMaster.health = pMaster.maxHealth;
        }
    }

    public void DeductHealth(int change)
    {
        if (pMaster.health != 0)
        {
            pMaster.health -= change;
            if (pMaster.health + change < 0)
                pMaster.health = 0;
        }
        if (pMaster.health == 0)
            onDeath();
    }

    public void onDeath()
    {
        Debug.Log("You died...");
    }
}
