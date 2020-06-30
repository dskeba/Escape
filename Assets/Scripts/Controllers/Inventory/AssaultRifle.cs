
using UnityEngine;

public class AssaultRifle : InventoryItemGun
{
    private Sprite _image;

    public override string Name {
        get
        {
            return "Assault Rifle";
        }
    }

    public override Sprite Image
    {
        get
        {
            return _image;
        }
    }

    protected override void OnGunAwake() {
        base.useRate = 0.2f;
        base.autoUse = true;
        _image = Resources.Load<Sprite>("Sprites/Assault_Rifle");
    }

    protected override void OnGunStart() { }

    protected override void OnGunFixedUpdate() { }

    protected override void OnGunUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
    }
}