using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public int recourceStackSize = 1000;

    private void Awake()
    {
        buildItemDatabase();
    }

    public Item GetItem(int itemID)
    {
        return items.Find(item => item.itemID == itemID);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    void buildItemDatabase()
    {
        items = new List<Item>{

            new Item(0, "Sword", "Just a test weapon",
            new Dictionary<string, int>
            {
                {"Damage", 10 },
                {"Speed", 2 }
            }),

            new Item(1, "Armor", "Just test armor",
            new Dictionary<string, int>
            {
                {"Armor", 15 },
                {"Defence", 8 }
            }),

            new Item(2, "Wood", "A piece of wood",
            new Dictionary<string, int>
            {
                {"StackSize", recourceStackSize }
            }),
            
            new Item(3, "Iron Ore", "A piece of iron ore",
            new Dictionary<string, int>
            {
                {"StackSize", recourceStackSize }
            }),
            
            new Item(4, "Iron Fragments", "Melted iron, used to make items, also for upgrades",
            new Dictionary<string, int>
            {
                {"StackSize", recourceStackSize }
            }),

            new Item(5, "Charcoal", "A piece of charcoal",
            new Dictionary<string, int>
            {
                {"StackSize", recourceStackSize }
            })
        };
    }
}
