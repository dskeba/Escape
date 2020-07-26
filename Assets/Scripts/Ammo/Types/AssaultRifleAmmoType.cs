using UnityEngine;

public class AssaultRifleAmmoType : IAmmoType
{
    public AmmoName Name { get => AmmoName.AssaultRifle; }
    public Sprite Image { get => Resources.Load<Sprite>("Sprites/rifle-bullet-icon"); }
}