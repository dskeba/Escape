
public class AssaultRifleAmmo : AmmoBase
{
    public AssaultRifleAmmo()
    {
        base.Name = "Rifle Ammo";
        base.Type = AmmoType.AssaultRifle;
        base.Quantity = 30;
    }

    protected override void OnBaseAwake() { }

    protected override void OnBaseFixedUpdate() { }

    protected override void OnBaseStart() { }

    protected override void OnBaseUpdate() { }
}