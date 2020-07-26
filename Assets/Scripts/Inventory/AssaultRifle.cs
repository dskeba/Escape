
using UnityEngine;

public class AssaultRifle : Gun
{
    public AssaultRifle()
    {
        base.Name = "Assault Rifle";
        base.UseRate = 0.2f;
        base.IsAuto = true;
        base.AmmoType = new AssaultRifleAmmoType();
        base.MagSize = 30;
        base.AmmoLoaded = 30;
        base.ReloadTime = 2f;
        base.MaxBloom = 0.01f;
        base.MinBloom = 0f;
        base.BloomResetTime = 1f;
        base.BulletsPerShot = 1;
    }

    protected override void OnGunAwake() {
        base.Image = Resources.Load<Sprite>("Sprites/Assault_Rifle");
    }

    protected override void OnGunStart() { }

    protected override void OnGunFixedUpdate() { }

    protected override void OnGunUpdate() { }

    protected override void OnGunShoot()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
    }
}