using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    Idle,
    Patrol,
    Aggro,
    Attack,
    Dead
}

public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _stopDistance = 3f;
    private bool _isAttacking = false;
    private float _distanceToAggro = 25f;
    private float _distanceToAttack = 3f;
    private float _patrolRadius = 25f;
    private float _currentPatrolTime = 0f;
    private float _maxPatrolTime = 10f;
    private Vector3 _patrolDestination;
    private ZombieState _state = ZombieState.Idle;

    public Transform player;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        SetupNavMeshAgent();
    }

    private void Idle()
    {
        if (_state == ZombieState.Idle) { return; }
        Debug.Log("First to Idle");
        _state = ZombieState.Idle;
        _agent.speed = 0f;
        _agent.isStopped = true;
    }

    private void TransitionToAggro()
    {
        _state = ZombieState.Aggro;
        _animator.SetBool("IsAttacking", false);
        _agent.speed = 8f;
        _agent.SetDestination(player.position);
        _agent.isStopped = false;
    }

    private void Aggro()
    {
        if (_state == ZombieState.Aggro) {
            _agent.SetDestination(player.position);
            return;
        }
        TransitionToAggro();
    }

    private void TransitionToPatrol()
    {
        _currentPatrolTime = 0f;
        _state = ZombieState.Patrol;
        _agent.speed = 2f;
        _agent.isStopped = false;
        Vector3 randomInsideSphere = Random.insideUnitSphere;
        randomInsideSphere.y = 0;
        _patrolDestination = transform.position + (randomInsideSphere * _patrolRadius);
        _agent.SetDestination(_patrolDestination);
    }

    private void Patrol()
    {
        if (_state == ZombieState.Patrol) {
            _currentPatrolTime += Time.fixedDeltaTime;
            if (_currentPatrolTime > _maxPatrolTime)
            {
                Idle();
            }
            float distanceToPatrol = Vector3.Distance(transform.position, _patrolDestination);
            if (distanceToPatrol < _stopDistance)
            {
                Idle();
            }
            return;
        }
        TransitionToPatrol();
    }

    private void TransitionToAttack() {
        _state = ZombieState.Attack;
        _agent.isStopped = true;
        StartCoroutine(DoAttack());
    }

    private void Attack()
    {
        if (_state == ZombieState.Attack)
        {
            return;
        }
        TransitionToAttack();
    }

    private void SetupNavMeshAgent()
    {
        _agent.speed = 8f;
        _agent.acceleration = 100f;
        _agent.SetDestination(player.position);
    }

    private void FixedUpdate()
    {
        if (_state == ZombieState.Dead) { return; }
        if (_state == ZombieState.Attack) { return; }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < _distanceToAttack)
        {
            Attack();
        }
        else if (distanceToPlayer < _distanceToAggro)
        {
            Aggro();
        } 
        else
        {
            Patrol();
        }
        _animator.SetFloat("Speed", _agent.speed);
    }

    public void Kill()
    {
        _agent.isStopped = true;
        _state = ZombieState.Dead;
    }

    private IEnumerator DoAttack()
    {
        _animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(5);
        _animator.SetBool("IsAttacking", false);
        Idle();
    }
}