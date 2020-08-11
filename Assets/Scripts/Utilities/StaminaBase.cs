
using System;
using UnityEngine;

public abstract class StaminaBase : MonoBehaviour
{
    public event EventHandler<StaminaEvent> OnRemoveStamina;
    public event EventHandler<StaminaEvent> OnAddStamina;

    public StaminaBase() { }

    protected abstract void OnBaseRemoveStamina(float amount, Vector3 position);
    protected abstract void OnBaseAddStamina(float amount, Vector3 position);

    public float CurrentStamina { get; set; }
    public float MaxStamina { get; set; }

    public void RemoveStamina(float amount, Vector3 position)
    {
        CurrentStamina -= amount;
        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
        OnBaseRemoveStamina(amount, position);
        if (OnRemoveStamina != null)
        {
            OnRemoveStamina(this, new StaminaEvent(this));
        }
    }

    public void AddStamina(float amount, Vector3 position)
    {
        CurrentStamina += amount;
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        OnBaseAddStamina(amount, position);
        if (OnAddStamina != null)
        {
            OnAddStamina(this, new StaminaEvent(this));
        }
    }
}
