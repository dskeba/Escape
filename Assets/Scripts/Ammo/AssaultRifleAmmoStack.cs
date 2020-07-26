
public class AssaultRifleAmmoStack : AmmoStackBase
{
    public AssaultRifleAmmoStack()
    {
        base.Name = "Rifle Ammo";
        base.AmmoType = new AssaultRifleAmmoType();
        base.Quantity = 30;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}