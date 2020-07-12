
public interface IAmmo
{
    string Name { get; set; }
    AmmoType Type { get; set; }
    int Quantity { get; set; }

    void OnPickup();
}

public enum AmmoType
{
    Pistol,
    AssaultRifle
}