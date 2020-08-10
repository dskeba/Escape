using UnityEngine;

public class PistolAmmoType : IAmmoType
{
    public AmmoName Name { get => AmmoName.Pistol; }
    public Sprite Image { get => Resources.Load<Sprite>("Sprites/pistol-bullet-icon"); }
}