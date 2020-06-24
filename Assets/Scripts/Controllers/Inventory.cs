using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 5;

    private List<IInventoryItem> items;
    private int equippedItemIndex = -1;
    private IInventoryItem equippedItem;

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemEquipped;
    public event EventHandler<InventoryEventArgs> ItemUnequipped;

    public Inventory()
    {
        items = new List<IInventoryItem>();
/*        for (int i = 0; i < SLOTS; i++)
        {
            items.Insert(i, null);
        }*/
    }

    public void AddItem(IInventoryItem item)
    {
        if (items.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                items.Insert(0, item);
                Debug.Log(items[0]);
                item.OnPickup();
                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void EquipItem(int index, IInventoryItem item)
    {
        equippedItemIndex = index;
        equippedItem = item;
        if (ItemEquipped != null)
        {
            ItemEquipped(this, new InventoryEventArgs(item));
        }
    }

    public void UnequipItem()
    {
        if (ItemUnequipped != null)
        {
            ItemUnequipped(this, new InventoryEventArgs(equippedItem));
        }
        equippedItemIndex = -1;
        equippedItem = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1") && items.ElementAtOrDefault(0) != null)
        {
            if (equippedItemIndex == 0)
            {
                UnequipItem();
            }
            else
            {
                EquipItem(0, items[0]);
            }
        }
    }
}
