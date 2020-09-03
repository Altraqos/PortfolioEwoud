using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    PlayerMaster pMaster;
    PlayerMovement pMovement;

    public Text healthText;
    public Text staminaText;
    public Text hungerText;
    public Text thirstText;

    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;
    public Slider thirstBar;

    public Canvas inventoryCanvas;

    public bool showText = true;
    public bool showPercentages = false;
    public bool inventoryOpen = false;

    //Tijdelijk remove on build
    public void Update()
    {
        hideText(showText);
        openInventory();
    }

    public void Start()
    {
        pMaster = GetComponent<PlayerMaster>();
        pMovement = GetComponent<PlayerMovement>();
        inventoryCanvas.enabled = false;
    }

    public void openInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryOpen = !inventoryOpen;
            if (!inventoryOpen)
            {
                inventoryCanvas.enabled = false;
                pMovement.lockMouse = false;
            }
            if (inventoryOpen)
            {
                inventoryCanvas.enabled = true;
                pMovement.lockMouse = true;
            }
        }
    }

    public void hideText(bool show)
    {
        healthText.enabled = show;  
        staminaText.enabled = show;  
        hungerText.enabled = show;  
        thirstText.enabled = show;  
    }

    public void SetUI()
    {
        if (!showPercentages)
        {
            healthText.text = pMaster.health + " / " + pMaster.maxHealth;
            staminaText.text = pMaster.stamina + " / " + pMaster.maxStamina;
            hungerText.text = pMaster.hunger + " / " + pMaster.maxHunger;
            thirstText.text = pMaster.thirst + " / " + pMaster.maxThirst;
        }
        if (showPercentages)
        {
            healthText.text = (pMaster.health / pMaster.maxHealth * 100) + "%";
            staminaText.text = (pMaster.stamina / pMaster.maxStamina * 100) + "%";
            hungerText.text = (pMaster.hunger / pMaster.maxHunger * 100) + "%";
            thirstText.text = (pMaster.thirst / pMaster.maxThirst * 100) + "%";
        }

        healthBar.maxValue = pMaster.maxHealth;
        healthBar.value = pMaster.health;
        staminaBar.maxValue = pMaster.maxStamina;
        staminaBar.value = pMaster.stamina; 
        hungerBar.maxValue = pMaster.maxHunger;
        hungerBar.value = pMaster.hunger;  
        thirstBar.maxValue = pMaster.maxThirst;
        thirstBar.value = pMaster.thirst;
    }
}
