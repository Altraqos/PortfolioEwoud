using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> playerItems = new List<Item>();
    public ItemManager iManager;
    public ItemUIManager iUIManager;

    public void Start()
    {
        iUIManager.GetComponent<ItemUIManager>();
        GiveItem(0);
        GiveItem(1);
    }

    public void GiveItem(int itemID)
    {
        Item itemToAdd = iManager.GetItem(itemID);
        playerItems.Add(itemToAdd);
        iUIManager.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.itemName);
    }
    public void GiveItem(string itemName)
    {
        Item itemToAdd = iManager.GetItem(itemName);
        playerItems.Add(itemToAdd);
        iUIManager.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.itemName);
    }

    public Item CheckForItem(int itemID)
    {
        return playerItems.Find(item => item.itemID == itemID);
    }
    
    public Item CheckForItem(string itemName)
    {
        return playerItems.Find(item => item.itemName == itemName);
    }

    public void RemoveItem(int itemID)
    {
        Item item = CheckForItem(itemID);
        if (item != null)
        {
            playerItems.Remove(item);
            iUIManager.RemoveNewItem(item);
            Debug.Log("Removed item: " + item.itemName);
        }
    }
    
    public void RemoveItem(string itemName)
    {
        Item item = CheckForItem(itemName);
        if (item != null)
        {
            playerItems.Remove(item);
            iUIManager.RemoveNewItem(item);
            Debug.Log("Removed item: " + item.itemName);
        }
    }
}
