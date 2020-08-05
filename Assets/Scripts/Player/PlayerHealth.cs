using UnityEngine;

public class PlayerHealth : HealthBase
{
    private GameObject bloodObject;

    public PlayerHealth()
    {
        base.MaxHealth = 100;
        base.CurrentHealth = base.MaxHealth;
    }

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
    }

    protected override void OnBaseTakeDamage(int damage, Vector3 position)
    {
        Debug.Log("player has taken " + damage + " damage");
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    protected override void OnBaseDie()
    {
        Debug.Log("Player has died");
    }

    protected override void OnBaseRevive() { }
}
