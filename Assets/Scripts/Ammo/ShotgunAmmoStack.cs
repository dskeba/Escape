
public class ShotgunAmmoStack : AmmoStackBase
{
    public ShotgunAmmoStack() {
        base.Name = "Shotgun Ammo";
        base.AmmoType = new ShotgunAmmoType();
        base.Quantity = 5;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}
