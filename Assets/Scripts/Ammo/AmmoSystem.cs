
using System.Collections.Generic;
using System;

public class AmmoSystem : Singleton<AmmoSystem>
{
    public event EventHandler<AmmoSystemEvent> AmmoAdded;
    public event EventHandler<AmmoSystemEvent> AmmoUsed;

    private Dictionary<AmmoType, int> currentQuantity;
    private Dictionary<AmmoType, int> maxQuantity;

    public AmmoSystem()
    {
        currentQuantity = new Dictionary<AmmoType, int>();
        currentQuantity.Add(AmmoType.AssaultRifle, 0);
        currentQuantity.Add(AmmoType.Pistol, 0);

        maxQuantity = new Dictionary<AmmoType, int>();
        maxQuantity.Add(AmmoType.AssaultRifle, 240);
        maxQuantity.Add(AmmoType.Pistol, 120);
    }

    public int GetQuantity(AmmoType type)
    {
        return currentQuantity[type];
    }

    public void AddAmmo(IAmmo ammo)
    {
        AddAmmo(ammo.Type, ammo.Quantity);
        ammo.OnPickup();
    }

    public void AddAmmo(AmmoType type, int quantity)
    {
        currentQuantity[type] += quantity;
        if (currentQuantity[type] > maxQuantity[type])
        {
            currentQuantity[type] = maxQuantity[type];
        }
        else
        {
            if (AmmoAdded != null)
            {
                AmmoAdded(this, new AmmoSystemEvent(type));
            }
        }
    }

    public bool RemoveAmmo(AmmoType type, int quantity)
    {
        if (currentQuantity[type] <= 0)
        {
            return false;
        }
        currentQuantity[type] -= quantity;
        if (AmmoUsed != null)
        {
            AmmoUsed(this, new AmmoSystemEvent(type));
        }
        return true;
    }
}
