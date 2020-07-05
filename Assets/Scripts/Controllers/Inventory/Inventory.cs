using UnityEngine;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemEquipped;
    public event EventHandler<InventoryEventArgs> ItemUnequipped;
    public event EventHandler<InventoryEventArgs> ItemDropped;

    private const int MAX_ITEMS = 9;
    private Dictionary<int, IInventoryItem> items;
    private int equippedItemIndex = -1;

    public Inventory()
    {
        items = new Dictionary<int, IInventoryItem>();
    }

    public void AddItem(IInventoryItem item)
    {
        if (items.Count < MAX_ITEMS)
        {
            for (int i = 0; i < MAX_ITEMS; i++)
            {
                if (!items.ContainsKey(i))
                {
                    items.Add(i, item);
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
        if (!items.ContainsKey(index)) { return; }
        if (equippedItemIndex == index) {
            UnequipItem(equippedItemIndex);
            return;
        } else if (equippedItemIndex >= 0) {
            UnequipItem(equippedItemIndex);
        }
        equippedItemIndex = index;
        items[equippedItemIndex].IsEquipped = true;
        if (ItemEquipped != null)
        {
            ItemEquipped(this, new InventoryEventArgs(items[equippedItemIndex]));
        }
    }

    public void UnequipItem(int index)
    {
        items[index].IsEquipped = false;
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
        items.Remove(equippedItemIndex);
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
