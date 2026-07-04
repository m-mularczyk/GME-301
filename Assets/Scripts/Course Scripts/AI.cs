using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AI : MonoBehaviour
{
    private enum AIState
    {
        Walking,
        Jumping,
        Attack,
        Death
    }

    private NavMeshAgent _agent;
    [SerializeField] private AIState _currentAIState = AIState.Walking;

    [SerializeField] private int _currentDestinationIndex = 0;
    private bool _normalWaypointsOrder = true;

    [SerializeField] private List<Transform> _waypoints;

    private bool _isAttacking = false;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        if( _agent == null)
        {
            Debug.LogError("NavMeshAgent not found");
        }

        var randomTarget = _waypoints[_currentDestinationIndex].position;
        _agent.destination = randomTarget;
    }

    void Update()
    {

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _agent.isStopped = true;
            _currentAIState = AIState.Jumping;
        }

        // Determine current AI State
        switch (_currentAIState)
        {
            case AIState.Walking:
                Debug.Log("Walking...");
                CalculateAIMovement();
                break;

            case AIState.Jumping:
                Debug.Log("Jumping...");
                break;

            case AIState.Attack:
                Debug.Log("Attacking...");
                if(_isAttacking == false)
                {
                    StartCoroutine(AttackRoutine());
                    _isAttacking = true;
                }
                break;

            case AIState.Death:
                Debug.Log("Dead");
                break;
        }
    }

    private void CalculateAIMovement()
    {
        if (_agent.remainingDistance < 0.5f) // Normal waypoints order
        {
            if (_normalWaypointsOrder)
            {
                ForwardOrder();
            }
            else // Reverse waypoints order
            {
                ReverseOrder();
            }

            _agent.SetDestination(_waypoints[_currentDestinationIndex].position);


            // Perform attack
            _currentAIState = AIState.Attack;
        }
    }

    private void ReverseOrder()
    {
        if (_currentDestinationIndex == 0)
        {
            _normalWaypointsOrder = true;
            _currentDestinationIndex++;
        }
        else
        {
            _currentDestinationIndex--;
        }
    }

    private void ForwardOrder()
    {
        if (_currentDestinationIndex == _waypoints.Count - 1)
        {
            _normalWaypointsOrder = false;
            _currentDestinationIndex--;
        }
        else
        {
            _currentDestinationIndex++;
        }
    }

    IEnumerator AttackRoutine()
    {
        _agent.isStopped = true;
        _currentAIState = AIState.Attack;
        Debug.Log("Started attacking");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Attack finished, started walking");
        _agent.isStopped = false;
        _currentAIState = AIState.Walking;
        _isAttacking = false;
    }
}
