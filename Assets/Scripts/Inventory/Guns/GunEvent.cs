
using System;

public class GunEvent : EventArgs
{
    public Gun Gun;

    public GunEvent(Gun gun)
    {
        Gun = gun;
    }
}