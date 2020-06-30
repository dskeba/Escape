using UnityEngine;

public class Medkit : InventoryItemConsumable
{
    private Sprite _image;

    public override string Name
    {
        get
        {
            return "Medkit";
        }
    }

    public override Sprite Image
    {
        get
        {
            return _image;
        }
    }


    protected override void OnConsumableAwake()
    {
        base.useRate = 1f;
        base.autoUse = false;
        _image = Resources.Load<Sprite>("Sprites/Assault_Rifle");
    }

    protected override void OnConsumableFixedUpdate()
    {
    }

    protected override void OnConsumableStart()
    {
    }

    protected override void OnConsumableUpdate()
    {
    }

    protected override void OnUse()
    {
        Debug.Log("MEDKIT USED");
    }

}
