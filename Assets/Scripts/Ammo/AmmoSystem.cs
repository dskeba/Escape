using UnityEngine;
using System.Collections.Generic;
using System;

public class AmmoSystem : MonoBehaviour
{
    public event EventHandler<AmmoSystemEvent> AmmoAdded;
    public event EventHandler<AmmoSystemEvent> AmmoUsed;

    private const int MAX_PISTOL_AMMO = 120;
    private const int MAX_ASSAULT_RIFLE_AMMO = 240;
    private Dictionary<AmmoType, int> ammoQuantity;

    public AmmoSystem()
    {
        ammoQuantity = new Dictionary<AmmoType, int>();
    }

    public void AddAmmo(IAmmo ammo)
    {
        if (ammo.Type == AmmoType.Pistol)
        {
            ammoQuantity[AmmoType.Pistol] += ammo.Quantity;
            if (ammoQuantity[AmmoType.Pistol] > MAX_PISTOL_AMMO)
            {
                ammoQuantity[AmmoType.Pistol] = MAX_PISTOL_AMMO;
            }
            else
            {
                AmmoAdded(this, new AmmoSystemEvent(ammo));
            }
        } 
        else if (ammo.Type == AmmoType.AssaultRifle)
        {
            ammoQuantity[AmmoType.AssaultRifle] += ammo.Quantity;
            if (ammoQuantity[AmmoType.AssaultRifle] > MAX_ASSAULT_RIFLE_AMMO)
            {
                ammoQuantity[AmmoType.AssaultRifle] = MAX_ASSAULT_RIFLE_AMMO;
            }
            else
            {
                AmmoAdded(this, new AmmoSystemEvent(ammo));
            }
        }
    }

    public void UseAmmo(AmmoType type)
    {
        
    }
}
