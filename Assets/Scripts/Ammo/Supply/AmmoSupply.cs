
using System.Collections.Generic;
using System;

public class AmmoSupply : Singleton<AmmoSupply>
{
    public event EventHandler<AmmoSupplyEvent> AmmoAdded;
    public event EventHandler<AmmoSupplyEvent> AmmoRemoved;

    private Dictionary<AmmoName, int> currentQuantity;
    private Dictionary<AmmoName, int> maxQuantity;

    public AmmoSupply()
    {
        currentQuantity = new Dictionary<AmmoName, int>();
        currentQuantity.Add(AmmoName.AssaultRifle, 0);
        currentQuantity.Add(AmmoName.Pistol, 0);
        currentQuantity.Add(AmmoName.Shotgun, 0);

        maxQuantity = new Dictionary<AmmoName, int>();
        maxQuantity.Add(AmmoName.AssaultRifle, 240);
        maxQuantity.Add(AmmoName.Pistol, 120);
        maxQuantity.Add(AmmoName.Shotgun, 60);
    }

    public int GetQuantity(AmmoName name)
    {
        return currentQuantity[name];
    }

    public void AddAmmo(IAmmoStack ammoStack)
    {
        AddAmmo(ammoStack.AmmoType.Name, ammoStack.Quantity);
        ammoStack.OnPickup();
    }

    public void AddAmmo(AmmoName name, int quantity)
    {
        currentQuantity[name] += quantity;
        if (currentQuantity[name] > maxQuantity[name])
        {
            currentQuantity[name] = maxQuantity[name];
        }
        else
        {
            if (AmmoAdded != null)
            {
                AmmoAdded(this, new AmmoSupplyEvent(name));
            }
        }
    }

    public bool RemoveAmmo(AmmoName name, int quantity)
    {
        if (currentQuantity[name] <= 0)
        {
            return false;
        }
        currentQuantity[name] -= quantity;
        if (AmmoRemoved != null)
        {
            AmmoRemoved(this, new AmmoSupplyEvent(name));
        }
        return true;
    }
}
