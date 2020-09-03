using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    PlayerMaster pMaster;
    PlayerUI pUI;

    public void Start()
    {
        pMaster = GetComponent<PlayerMaster>();
        pUI = GetComponent<PlayerUI>();
    }

    public void IncreaseStamina(int change)
    {
        if (pMaster.stamina != pMaster.maxStamina)
        {
            pMaster.stamina += change;
            if (pMaster.stamina + change > pMaster.maxStamina)
                pMaster.stamina = pMaster.maxStamina;
        }
        pUI.SetUI();
    }

    public void DeductStamina(int change)
    {
        if (pMaster.stamina != 0)
        {
            pMaster.stamina -= change;
            if (pMaster.stamina + change < 0)
                pMaster.stamina = 0;
        }
        if (pMaster.stamina == 0)
            Debug.Log("Out of stamina");
        pUI.SetUI();
    }
}
