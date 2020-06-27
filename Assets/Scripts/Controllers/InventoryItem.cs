using UnityEngine;
using System;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    bool IsEquipped { get; set; }
    void OnPickup();
    void OnDrop();
}

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item;

    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}
