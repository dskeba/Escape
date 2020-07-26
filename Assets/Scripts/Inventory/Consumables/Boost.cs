using UnityEngine;

public class Boost : Consumable
{
    public Boost()
    {
        base.Name = "Boost";
        base.UseRate = 1f;
        base.IsAuto = false;
    }

    protected override void OnConsumableAwake()
    {
        base.Image = Resources.Load<Sprite>("Sprites/Boost");
    }

    protected override void OnConsumableFixedUpdate() { }

    protected override void OnConsumableStart() { }

    protected override void OnConsumableUpdate() { }

    protected override void OnUse()
    {
        Debug.Log("USE BOOST");
    }

    protected override void OnUsableDrop() { }
}
