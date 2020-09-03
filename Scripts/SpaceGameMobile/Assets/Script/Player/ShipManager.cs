using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    public Text moneyText;
    public int money = 0;

    public void OnEnable()
    {
        SetUI();
    }


    public void IncreaseMoney(int change)
    {
        money += change;
        SetUI();
    }

    public void DeductMoney(int change)
    {
        if (money - change > 0)
            money -= change;
        else
            Debug.Log("Sorry can't do that");
        SetUI();
    }

    public void SetUI()
    {
        moneyText.text = "$" + money.ToString();
    }
}
