using UnityEngine;

public class ZombieHealth : HealthBase
{
    private GameObject bloodObject;
    private Animator _animator;
    private Zombie _zombie;

    public ZombieHealth() {
        base.MaxHealth = 100;
        base.CurrentHealth = base.MaxHealth;
    }

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
        _animator = GetComponent<Animator>();
        _zombie = GetComponent<Zombie>();
    }

    protected override void OnBaseDamage(int damage, Vector3 position)
    {
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    protected override void OnBaseHeal(int damage, Vector3 position)
    {

    }

    protected override void OnBaseDie()
    {
        Debug.Log("Zombie has died");
        _animator.SetBool("IsAlive", false);
        _zombie.Kill();
    }

    protected override void OnBaseRevive() { }
}
