using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _stopDistance = 3f;
    private bool _isAttacking = false;

    public Transform goal;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        SetupNavMeshAgent();
    }

    private void SetupNavMeshAgent()
    {
        _agent.speed = 8f;
        _agent.autoBraking = true;
        _agent.acceleration = 100f;
        _agent.stoppingDistance = _stopDistance;
        _agent.destination = goal.position;
    }

    private void FixedUpdate()
    {
        _agent.destination = goal.position;
        _animator.SetFloat("Speed", _agent.speed);
        if (_agent.remainingDistance < _stopDistance)
        {
            _agent.speed = 0f;
            StartCoroutine(Attack());
        } 
        else if (!_isAttacking)
        {
            _agent.speed = 8f;
        }
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        _animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(5);
        _isAttacking = false;
        _animator.SetBool("IsAttacking", false);
    }
}