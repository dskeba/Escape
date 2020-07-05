﻿
using UnityEngine;

public class AssaultRifle : Gun
{
    public AssaultRifle()
    {
        base.Name = "Assault Rifle";
        base.UseRate = 0.2f;
        base.IsAuto = true;
    }

    protected override void OnGunAwake() {
        base.Image = Resources.Load<Sprite>("Sprites/Assault_Rifle");
    }

    protected override void OnGunStart() { }

    protected override void OnGunFixedUpdate() { }

    protected override void OnGunUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
    }
}