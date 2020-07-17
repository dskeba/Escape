
public class ShotgunAmmo : AmmoBase
{
    public ShotgunAmmo() {
        base.Name = "Shotgun Ammo";
        base.Type = AmmoType.Shotgun;
        base.Quantity = 5;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}
