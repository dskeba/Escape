
using System;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public event EventHandler<HealthEvent> OnDamage;
    public event EventHandler<HealthEvent> OnHeal;
    public event EventHandler<HealthEvent> OnDie;

    public HealthBase() { }

    protected abstract void OnBaseDamage(int damage, Vector3 position);
    protected abstract void OnBaseHeal(int damage, Vector3 position);
    protected abstract void OnBaseDie();
    protected abstract void OnBaseRevive();

    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }

    public bool IsAlive { 
        get
        {
            return (CurrentHealth > 0);
        }
    }

    public void Damage(int amount, Vector3 position)
    {
        if (!IsAlive) { return; }
        CurrentHealth -= amount;
        OnBaseDamage(amount, position);
        if (!IsAlive)
        {
            Die();
        }
        if (OnDamage != null)
        {
            OnDamage(this, new HealthEvent(this));
        }
    }

    public void Heal(int amount, Vector3 position)
    {
        if (!IsAlive) { return; }
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        OnBaseHeal(amount, position);
        if (OnHeal != null)
        {
            OnHeal(this, new HealthEvent(this));
        }
    }

    public void Die()
    {
        OnBaseDie();
        if (OnDie != null)
        {
            OnDie(this, new HealthEvent(this));
        }
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
        OnBaseRevive();
    }
}
