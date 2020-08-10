using UnityEngine;

public class ShotgunAmmoType : IAmmoType
{
    public AmmoName Name { get => AmmoName.Shotgun; }
    public Sprite Image { get => Resources.Load<Sprite>("Sprites/shotgun-bullet-icon"); }
}