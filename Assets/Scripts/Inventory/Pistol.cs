
using UnityEngine;

public class Pistol : Gun
{
    public Pistol()
    {
        base.Name = "Pistol";
        base.UseRate = 0.2f;
        base.IsAuto = false;
        base.AmmoType = AmmoType.Pistol;
        base.MagSize = 10;
        base.AmmoLoaded = 10;
        base.ReloadTime = 2f;
    }

    protected override void OnGunAwake()
    {
        base.Image = Resources.Load<Sprite>("Sprites/Pistol");
    }

    protected override void OnGunStart() { }

    protected override void OnGunFixedUpdate() { }

    protected override void OnGunUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/pistol_shot", 0.5f);

    }
}
