using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private const int MAX_ITEMS = 9;

    private List<IInventoryItem> items;
    private int equippedItemIndex = -1;

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemEquipped;
    public event EventHandler<InventoryEventArgs> ItemUnequipped;
    public event EventHandler<InventoryEventArgs> ItemDropped;

    public Inventory()
    {
        items = new List<IInventoryItem>();
    }

    public void AddItem(IInventoryItem item)
    {
        if (items.Count < MAX_ITEMS)
        {
            for (int i = 0; i < MAX_ITEMS; i++)
            {
                if (items.ElementAtOrDefault(i) == null)
                {
                    items.Insert(i, item);
                    item.OnPickup();
                    if (ItemAdded != null)
                    {
                        ItemAdded(this, new InventoryEventArgs(item));
                    }
                    return;
                }
            }
        }
    }

    public void EquipItem(int index)
    {
        Debug.Log("equippedItemIndex" + equippedItemIndex);
        if (items.ElementAtOrDefault(index) == null) { return; }
        if (equippedItemIndex == index) {
            UnequipItem(equippedItemIndex);
            return;
        } else if (equippedItemIndex >= 0) {
            UnequipItem(equippedItemIndex);
        }
        Debug.Log("index" + index);
        equippedItemIndex = index;
        items[equippedItemIndex].IsEquipped = true;
        if (ItemEquipped != null)
        {
            ItemEquipped(this, new InventoryEventArgs(items[equippedItemIndex]));
        }
    }

    public void UnequipItem(int index)
    {
        Debug.Log("Unequip " + index);
        items[equippedItemIndex].IsEquipped = false;
        if (ItemUnequipped != null)
        {
            ItemUnequipped(this, new InventoryEventArgs(items[equippedItemIndex]));
        }
        equippedItemIndex = -1;
    }

    public void DropItem()
    {
        if (equippedItemIndex < 0) { return; }
        items[equippedItemIndex].IsEquipped = false;
        items[equippedItemIndex].OnDrop();
        if (ItemDropped != null)
        {
            ItemDropped(this, new InventoryEventArgs(items[equippedItemIndex]));
        }
        items.RemoveAt(equippedItemIndex);
        equippedItemIndex = -1;
    }

    private void Update()
    {
        CheckSlotKeys();
        CheckDropKey();
    }

    private void CheckSlotKeys()
    {
        for (int i = 0; i < MAX_ITEMS; i++)
        {
            String keyCode = (i + 1).ToString();
            if (Input.GetKeyDown(keyCode))
            {
                EquipItem(i);
            }
        }
    }

    private void CheckDropKey()
    {
        if (Input.GetKeyDown("g"))
        {
            DropItem();
        }
    }
}
