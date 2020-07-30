
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public HealthBase() { }

    protected abstract void OnBaseTakeDamage(int damage, Vector3 position);
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

    public void TakeDamage(int damage, Vector3 position)
    {
        if (!IsAlive) { return; }
        CurrentHealth -= damage;
        OnBaseTakeDamage(damage, position);
        if (!IsAlive)
        {
            Die();
        }
    }

    public void Die()
    {
        OnBaseDie();
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
        OnBaseRevive();
    }
}
