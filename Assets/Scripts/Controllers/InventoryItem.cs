using UnityEngine;
using System;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    bool Equiped { get; set; }
    void OnPickup();
}

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item;

    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}
