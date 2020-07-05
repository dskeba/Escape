
using UnityEngine;

public abstract class Usable : InventoryItemBase
{
    protected abstract void OnUsableAwake();
    protected abstract void OnUsableStart();
    protected abstract void OnUsableFixedUpdate();
    protected abstract void OnUsableUpdate();
    protected abstract void OnUse();

    private float _useRateTimer = 0f;
    private float _useRate = 1f;

    public Usable() { }

    public bool IsAuto { get; set; } = false;

    public float UseRate {
        get => _useRate;
        set => _useRateTimer = _useRate = value;
    }

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
        _useRateTimer += Time.deltaTime;

        if (_useRateTimer >= _useRate)
        {
            if (Input.GetButton("Fire1") && IsAuto ||
                Input.GetButtonDown("Fire1") && !IsAuto)
            {
                _useRateTimer = 0f;
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
