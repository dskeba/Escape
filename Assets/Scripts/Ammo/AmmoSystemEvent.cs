
using System;

public class AmmoSystemEvent : EventArgs
{
    public AmmoType Type;

    public AmmoSystemEvent(AmmoType type)
    {
        Type = type;
    }
}