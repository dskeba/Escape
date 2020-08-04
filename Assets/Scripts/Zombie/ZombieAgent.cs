using MonsterLove.StateMachine;
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

public class ZombieAgent : MonoBehaviour
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
    private StateMachine<ZombieState> _stateMachine;

    public Transform player;

    void Start()
    {
        _stateMachine = StateMachine<ZombieState>.Initialize(this);
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _stateMachine.ChangeState(ZombieState.Idle);

        SetupNavMeshAgent();
    }

    private void Idle_Enter()
    {
        Debug.Log("Idle_Enter");
        _animator.SetBool("IsAlive", true);
        _agent.speed = 0f;
        _agent.isStopped = true;
        _stateMachine.ChangeState(ZombieState.Patrol);
    }

    private void Aggro_Enter()
    {
        Debug.Log("Aggro_Enter");
        _animator.SetBool("IsAttacking", false);
        _agent.speed = 8f;
        _agent.SetDestination(player.position);
        _agent.isStopped = false;
    }

    private void Aggro_FixedUpdate()
    {
        Debug.Log("Aggro_FixedUpdate");
        _animator.SetFloat("Speed", _agent.speed);
        _agent.SetDestination(player.position);
        if (GetDistanceToPlayer() < _distanceToAttack)
        {
            _stateMachine.ChangeState(ZombieState.Attack);
        }
        if (GetDistanceToPlayer() > _distanceToAggro)
        {
            _stateMachine.ChangeState(ZombieState.Idle);
        }
    }

    private void Patrol_Enter()
    {
        Debug.Log("Patrol_Enter");
        _currentPatrolTime = 0f;
        _agent.speed = 2f;
        _agent.isStopped = false;
        Vector3 randomInsideSphere = Random.insideUnitSphere;
        randomInsideSphere.y = 0;
        _patrolDestination = transform.position + (randomInsideSphere * _patrolRadius);
        _agent.SetDestination(_patrolDestination);
    }

    private void Patrol_FixedUpdate()
    {
        Debug.Log("Patrol_FixedUpdate");
        _animator.SetFloat("Speed", _agent.speed);
        _currentPatrolTime += Time.fixedDeltaTime;
        if (_currentPatrolTime > _maxPatrolTime)
        {
            _stateMachine.ChangeState(ZombieState.Idle);
        }
        if (GetDistanceToPatrol() < _stopDistance)
        {
            _stateMachine.ChangeState(ZombieState.Idle);
        }
        if (GetDistanceToPlayer() < _distanceToAggro)
        {
            _stateMachine.ChangeState(ZombieState.Aggro);
        }
    }

    private void Attack_Enter() {
        _agent.isStopped = true;
        StartCoroutine(DoAttack());
    }

    private void Dead_Enter()
    {
        _agent.isStopped = true;
        _animator.SetBool("IsAlive", false);
    }

    public void Kill()
    {
        _stateMachine.ChangeState(ZombieState.Dead);
    }

    private void SetupNavMeshAgent()
    {
        _agent.speed = 8f;
        _agent.acceleration = 100f;
        _agent.SetDestination(player.position);
    }


    private IEnumerator DoAttack()
    {
        _animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(5);
        _animator.SetBool("IsAttacking", false);
        _stateMachine.ChangeState(ZombieState.Idle);
    }

    private float GetDistanceToPatrol()
    {
        return Vector3.Distance(transform.position, _patrolDestination);
    }

    private float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }
}