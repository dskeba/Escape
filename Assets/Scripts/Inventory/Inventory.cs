using UnityEngine;
using System.Collections.Generic;
using System;

public class Inventory : Singleton<Inventory>
{
    public event EventHandler<InventoryEvent> ItemAdded;
    public event EventHandler<InventoryEvent> ItemEquipped;
    public event EventHandler<InventoryEvent> ItemUnequipped;
    public event EventHandler<InventoryEvent> ItemDropped;

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
                    item.Index = i;
                    item.OnPickup();
                    if (ItemAdded != null)
                    {
                        ItemAdded(this, new InventoryEvent(item));
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
            UnequipItem(index);
            return;
        } else if (equippedItemIndex >= 0) {
            UnequipItem(equippedItemIndex);
        }
        items[index].IsEquipped = true;
        if (ItemEquipped != null)
        {
            ItemEquipped(this, new InventoryEvent(items[index]));
        }
        equippedItemIndex = index;
    }

    public void UnequipItem(int index)
    {
        items[index].IsEquipped = false;
        if (ItemUnequipped != null)
        {
            ItemUnequipped(this, new InventoryEvent(items[index]));
        }
        equippedItemIndex = -1;
    }

    public void DropItem(int index)
    {
        if (index < 0) { return; }
        UnequipItem(index);
        items[index].OnDrop();
        if (ItemDropped != null)
        {
            ItemDropped(this, new InventoryEvent(items[index]));
        }
        items.Remove(index);
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
            DropItem(equippedItemIndex);
        }
    }
}
