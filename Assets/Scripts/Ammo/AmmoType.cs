
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

public class PistolAmmoType : IAmmoType
{
    public AmmoName Name { get => AmmoName.Pistol; }
    public Sprite Image { get => Resources.Load<Sprite>("Sprites/pistol-bullet-icon"); }
}

public class ShotgunAmmoType : IAmmoType
{
    public AmmoName Name { get => AmmoName.Shotgun; }
    public Sprite Image { get => Resources.Load<Sprite>("Sprites/shotgun-bullet-icon"); }
}

public class AssaultRifleAmmoType : IAmmoType
{
    public AmmoName Name { get => AmmoName.AssaultRifle; }
    public Sprite Image { get => Resources.Load<Sprite>("Sprites/rifle-bullet-icon"); }
}

public static class AmmoTypes
{
    public static IAmmoType PistolAmmoType = new PistolAmmoType();
    public static IAmmoType ShotgunAmmoType = new ShotgunAmmoType();
    public static IAmmoType AssaultRifleAmmoType = new AssaultRifleAmmoType();
}