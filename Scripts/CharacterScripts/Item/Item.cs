using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public Dictionary<string, int> itemStats = new Dictionary<string, int>();

    public Item(int itemID, string itemName, string itemDescription, Dictionary<string, int> itemStats)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemIcon = Resources.Load<Sprite>("Sprites/" + itemName);
        this.itemStats = itemStats;
    }
    public Item(Item item)
    {
        this.itemID = item.itemID;
        this.itemName = item.itemName;
        this.itemDescription = item.itemDescription;
        this.itemIcon = Resources.Load<Sprite>("Sprites/" + itemName); ;
        this.itemStats = item.itemStats;
    }
}
