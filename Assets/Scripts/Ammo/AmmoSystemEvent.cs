
using System;

public class AmmoSystemEvent : EventArgs
{
    public IAmmo Ammo;

    public AmmoSystemEvent(IAmmo ammo)
    {
        Ammo = ammo;
    }
}