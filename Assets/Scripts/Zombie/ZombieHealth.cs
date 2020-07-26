using UnityEngine;

public class ZombieHealth : HealthBase
{
    private GameObject bloodObject;
    private Animator _animator;

    public ZombieHealth() {
        base.MaxHealth = 100;
        base.CurrentHealth = base.MaxHealth;
    }

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
        _animator = GetComponent<Animator>();
    }

    protected override void OnBaseTakeDamage(int damage, Vector3 position)
    {
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    protected override void OnBaseDie()
    {
        // TODO: Die
    }
}
