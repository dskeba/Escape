using UnityEngine;

public class Medkit : Consumable
{
    public Medkit()
    {
        base.Name = "Medkit";
        base.UseRate = 1f;
        base.IsAuto = false;
    }

    protected override void OnConsumableAwake()
    {
        base.Image = Resources.Load<Sprite>("Sprites/Medkit");
    }

    protected override void OnConsumableFixedUpdate() { }

    protected override void OnConsumableStart() { }

    protected override void OnConsumableUpdate() { }

    protected override void OnUse()
    {
        Debug.Log("USE MEDKIT");
    }

}
