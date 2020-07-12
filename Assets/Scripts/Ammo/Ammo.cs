
public interface IAmmo : IPickupObject
{
    AmmoType Type { get; set; }
    int Quantity { get; set; }
}

public enum AmmoType
{
    Pistol,
    AssaultRifle
}