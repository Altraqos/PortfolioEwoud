using UnityEngine;

public class PlayerMaster : MonoBehaviour 
{
    public int hunger;
    public int maxHunger = 100;

    public int thirst;
    public int maxThirst = 100;

    public int health;
    public int maxHealth = 100;

    public int stamina;
    public int maxStamina = 100;  

    public int currentInvSpace;
    public int maxInvSpace = 30;


    public void Start()
    {
        hunger = maxHunger;
        thirst = maxThirst;
        stamina = maxStamina;
        health = maxHealth;
    }
}
