using UnityEngine;

public abstract class InventoryItemConsumable : InventoryItemUsable
{
    protected abstract void OnConsumableAwake();
    protected abstract void OnConsumableStart();
    protected abstract void OnConsumableFixedUpdate();
    protected abstract void OnConsumableUpdate();

    protected override void OnUsableAwake()
    {
        base.ItemType = InventoryItemType.Consumable;
        OnConsumableAwake();
    }

    protected override void OnUsableStart()
    {
        OnConsumableStart();
    }

    protected override void OnUsableUpdate()
    {
        OnConsumableUpdate();
    }

    protected override void OnUsableFixedUpdate()
    {
        OnConsumableFixedUpdate();
    }
}
