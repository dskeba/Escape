
using UnityEngine;

public class Pistol : Gun
{
    public Pistol()
    {
        base.Name = "Pistol";
        base.UseRate = 0.2f;
        base.IsAuto = false;
        base.AmmoType = new PistolAmmoType();
        base.MagSize = 10;
        base.AmmoLoaded = 10;
        base.ReloadTime = 2f;
        base.MaxBloom = 0.01f;
        base.MinBloom = 0f;
        base.BloomResetTime = 0.5f;
        base.BulletsPerShot = 1;
    }

    protected override void OnGunAwake()
    {
        base.Image = Resources.Load<Sprite>("Sprites/Pistol");
    }

    protected override void OnGunStart() { }

    protected override void OnGunFixedUpdate() { }

    protected override void OnGunUpdate() { }

    protected override void OnGunShoot()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/pistol_shot", 0.5f);

    }
}
