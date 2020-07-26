
public interface IAmmoStack : IPickupObject
{
    IAmmoType AmmoType { get; set; }
    int Quantity { get; set; }
}