
using UnityEngine;

public interface IAmmoType
{
    AmmoName Name { get; }
    Sprite Image { get; }
}

public enum AmmoName
{
    Pistol,
    Shotgun,
    AssaultRifle
}