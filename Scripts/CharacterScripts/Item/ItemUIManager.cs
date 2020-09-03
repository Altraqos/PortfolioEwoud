using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    public List<ItemUI> UIItems = new List<ItemUI>();
    public GameObject slotPrefab;
    public Transform slotPanel;

    public void Awake()
    {
        PlayerMaster pMaster = GetComponent<PlayerMaster>();

        for (int i = 0; i < pMaster.maxInvSpace; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            UIItems.Add(instance.GetComponentInChildren<ItemUI>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        UIItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(UIItems.FindIndex(i => i.item == null), item);
    }
    
    public void RemoveNewItem(Item item)
    {
        UpdateSlot(UIItems.FindIndex(i => i.item == item), null);
    }
}
