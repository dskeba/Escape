using UnityEngine;

public class ZombieHealth : MonoBehaviour
{

    private int maxHealth = 100;
    private int minHealth = 0;
    private int currentHealth = 100;
    private GameObject bloodObject;

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
    }

    public void TakeDamage(int amount, Vector3 position)
    {
        currentHealth -= amount;
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
}
