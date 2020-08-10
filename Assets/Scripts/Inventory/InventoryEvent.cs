
using System;

public class InventoryEvent : EventArgs
{
    public IInventoryItem Item;

    public InventoryEvent(IInventoryItem item)
    {
        Item = item;
    }
}