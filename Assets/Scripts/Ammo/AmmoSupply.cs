
using System.Collections.Generic;
using System;

public class AmmoSupply : Singleton<AmmoSupply>
{
    public event EventHandler<AmmoSupplyEvent> AmmoAdded;
    public event EventHandler<AmmoSupplyEvent> AmmoRemoved;

    private Dictionary<AmmoType, int> currentQuantity;
    private Dictionary<AmmoType, int> maxQuantity;

    public AmmoSupply()
    {
        currentQuantity = new Dictionary<AmmoType, int>();
        currentQuantity.Add(AmmoType.AssaultRifle, 0);
        currentQuantity.Add(AmmoType.Pistol, 0);
        currentQuantity.Add(AmmoType.Shotgun, 0);

        maxQuantity = new Dictionary<AmmoType, int>();
        maxQuantity.Add(AmmoType.AssaultRifle, 240);
        maxQuantity.Add(AmmoType.Pistol, 120);
        maxQuantity.Add(AmmoType.Shotgun, 60);
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
                AmmoAdded(this, new AmmoSupplyEvent(type));
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
        if (AmmoRemoved != null)
        {
            AmmoRemoved(this, new AmmoSupplyEvent(type));
        }
        return true;
    }
}
