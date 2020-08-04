using UnityEngine;

public class ZombieHealth : HealthBase
{
    private GameObject bloodObject;
    private Animator _animator;
    private ZombieAgent _agent;

    public ZombieHealth() {
        base.MaxHealth = 100;
        base.CurrentHealth = base.MaxHealth;
    }

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
        _animator = GetComponent<Animator>();
        _agent = GetComponent<ZombieAgent>();
    }

    protected override void OnBaseTakeDamage(int damage, Vector3 position)
    {
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    protected override void OnBaseDie()
    {
        Debug.Log("Zombie has died");
        _animator.SetBool("IsAlive", false);
        _agent.Kill();
    }

    protected override void OnBaseRevive() { }
}
