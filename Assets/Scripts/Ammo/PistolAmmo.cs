
public class PistolAmmo : AmmoBase
{
    public PistolAmmo() {
        base.Name = "Pistol Ammo";
        base.Type = AmmoType.Pistol;
        base.Quantity = 10;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}
