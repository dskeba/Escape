
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public HealthBase() { }

    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    protected abstract void OnBaseDie();
    protected abstract void OnBaseTakeDamage(int damage, Vector3 position);

    public void TakeDamage(int damage, Vector3 position)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        OnBaseTakeDamage(damage, position);
    }

    public void Die()
    {
        OnBaseDie();
    }
}
