using UnityEngine;
using System;

public interface IInventoryItem
{
    string Name { get; set; }
    Sprite Image { get; set; }
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