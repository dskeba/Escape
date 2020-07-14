
using System;

public class AmmoSupplyEvent : EventArgs
{
    public AmmoType Type;

    public AmmoSupplyEvent(AmmoType type)
    {
        Type = type;
    }
}