using UnityEngine;

public abstract class Consumable : Usable
{
    public Consumable() { }

    protected abstract void OnConsumableAwake();
    protected abstract void OnConsumableStart();
    protected abstract void OnConsumableFixedUpdate();
    protected abstract void OnConsumableUpdate();

    protected override void OnUsableAwake()
    {
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
