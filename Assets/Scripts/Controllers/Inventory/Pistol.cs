
using UnityEngine;

public class Pistol : InventoryItemGun
{
    private Sprite _image;

    public override string Name
    {
        get
        {
            return "Pistol";
        }
    }

    public override Sprite Image
    {
        get
        {
            return _image;
        }
    }

    protected override void OnGunAwake()
    {
        base.useRate = 0.2f;
        base.autoUse = false;
        _image = Resources.Load<Sprite>("Sprites/Pistol");
    }

    protected override void OnGunStart() { }

    protected override void OnGunFixedUpdate() { }

    protected override void OnGunUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/pistol_shot", 0.5f);
    }
}
