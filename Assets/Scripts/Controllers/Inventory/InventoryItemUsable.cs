using UnityEngine;

public abstract class InventoryItemUsable : InventoryItemBase
{
    public float useRateTimer = 0f;

    protected float useRate = 1f;
    protected bool autoUse = false;
    protected abstract void OnUsableAwake();
    protected abstract void OnUsableStart();
    protected abstract void OnUsableFixedUpdate();
    protected abstract void OnUsableUpdate();
    protected abstract void OnUse();

    protected override void OnBaseAwake()
    {
        OnUsableAwake();
    }

    protected override void OnBaseStart()
    {
        OnUsableStart();
    }

    protected override void OnBaseUpdate()
    {
        useRateTimer += Time.deltaTime;
        if (useRateTimer >= useRate)
        {
            if (Input.GetButton("Fire1") && autoUse ||
                Input.GetButtonDown("Fire1") && !autoUse)
            {
                useRateTimer = 0f;
                OnUse();
            }
        }
        OnUsableUpdate();
    }

    protected override void OnBaseFixedUpdate()
    {
        OnUsableFixedUpdate();
    }
}
