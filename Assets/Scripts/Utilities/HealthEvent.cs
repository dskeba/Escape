
using System;

public class HealthEvent : EventArgs
{
    public HealthBase Health;

    public HealthEvent(HealthBase health)
    {
        Health = health;
    }
}