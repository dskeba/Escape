
public class PistolAmmoStack : AmmoStackBase
{
    public PistolAmmoStack() {
        base.Name = "Pistol Ammo";
        base.AmmoType = AmmoTypes.PistolAmmoType;
        base.Quantity = 10;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}
