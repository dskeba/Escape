using UnityEngine;
using System.Collections.Generic;
using System;

public class AmmoSystem : MonoBehaviour
{
    public event EventHandler<AmmoSystemEvent> AmmoAdded;
    public event EventHandler<AmmoSystemEvent> AmmoUsed;

    public Dictionary<AmmoType, int> ammoQuantity;
    public Dictionary<AmmoType, int> maxQuantity;

    public AmmoSystem()
    {
        ammoQuantity = new Dictionary<AmmoType, int>();
        ammoQuantity.Add(AmmoType.AssaultRifle, 0);
        ammoQuantity.Add(AmmoType.Pistol, 0);

        maxQuantity = new Dictionary<AmmoType, int>();
        maxQuantity.Add(AmmoType.AssaultRifle, 240);
        maxQuantity.Add(AmmoType.Pistol, 120);
    }

    public void AddAmmo(IAmmo ammo)
    {
        ammoQuantity[ammo.Type] += ammo.Quantity;
        ammo.OnPickup();
        if (ammoQuantity[ammo.Type] > maxQuantity[ammo.Type])
        {
            ammoQuantity[ammo.Type] = maxQuantity[ammo.Type];
        }
        else
        {
            if (AmmoAdded != null)
            {
                AmmoAdded(this, new AmmoSystemEvent(ammo.Type));
            }
        }
    }

    public void UseAmmo(AmmoType type, int quantity)
    {
        ammoQuantity[type] -= quantity;
        if (AmmoUsed != null)
        {
            AmmoUsed(this, new AmmoSystemEvent(type));
        }
    }
}
