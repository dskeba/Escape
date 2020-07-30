using UnityEngine;

public class ZombieHealth : HealthBase
{
    private GameObject bloodObject;
    private Animator _animator;
    private ZombieMovement _movement;

    public ZombieHealth() {
        base.MaxHealth = 100;
        base.CurrentHealth = base.MaxHealth;
    }

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
        _animator = GetComponent<Animator>();
        _movement = GetComponent<ZombieMovement>();
    }

    protected override void OnBaseTakeDamage(int damage, Vector3 position)
    {
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    protected override void OnBaseDie()
    {
        Debug.Log("Zombie has died");
        _animator.SetBool("IsAlive", false);
        _movement.Stop();
    }

    protected override void OnBaseRevive() { }
}
