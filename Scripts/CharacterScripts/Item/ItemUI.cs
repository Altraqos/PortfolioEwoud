using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler 
{
    public Item item;
    Image itemSprite;
    ItemUI selectedItem;

    public void OnEnable()
    {
        itemSprite = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<ItemUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            itemSprite.color = Color.white;
            itemSprite.sprite = this.item.itemIcon;
        }
        else
        {
            itemSprite.color = Color.clear;
        }
    }
}
