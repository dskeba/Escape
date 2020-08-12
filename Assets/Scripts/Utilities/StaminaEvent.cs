
using System;

public class StaminaEvent : EventArgs
{
    public StaminaBase Stamina;

    public StaminaEvent(StaminaBase stamina)
    {
        Stamina = stamina;
    }
}