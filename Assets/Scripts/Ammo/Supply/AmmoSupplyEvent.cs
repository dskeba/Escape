
using System;

public class AmmoSupplyEvent : EventArgs
{
    public AmmoName Name;

    public AmmoSupplyEvent(AmmoName name)
    {
        Name = name;
    }
}