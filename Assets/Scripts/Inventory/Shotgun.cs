
using UnityEngine;

public class Shotgun : Gun
{
    public Shotgun()
    {
        base.Name = "Shotgun";
        base.UseRate = 0.75f;
        base.IsAuto = false;
        base.AmmoType = AmmoType.Shotgun;
        base.MagSize = 5;
        base.AmmoLoaded = 5;
        base.ReloadTime = 2f;
        base.MaxBloom = 0.5f;
        base.MinBloom = 0.025f;
        base.BloomResetTime = 0.25f;
        base.BulletsPerShot = 5;
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
