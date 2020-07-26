
public class AssaultRifleAmmoStack : AmmoStackBase
{
    public AssaultRifleAmmoStack()
    {
        base.Name = "Rifle Ammo";
        base.AmmoType = AmmoTypes.AssaultRifleAmmoType;
        base.Quantity = 30;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}