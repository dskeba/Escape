
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{

    private int maxHealth = 100;
    private int minHealth = 0;
    private int currentHealth = 100;
    private bool alive = true;
    private GameObject bloodObject;

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
    }

    public void TakeDamage(int amount, Vector3 position)
    {
        currentHealth -= amount;
        if (currentHealth <= minHealth)
        {
            alive = false;
        }
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Revive()
    {
        alive = true;
    }
}
