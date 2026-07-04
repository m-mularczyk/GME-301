using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private NavMeshAgent _agent;

    [SerializeField] private int _currentWaypoint = 0;
    [SerializeField] private List<Transform> _waypoints;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if( _agent == null)
        {
            Debug.LogError("NavMeshAgent not found!");
        }

        // Heading towards first waypoint
        _agent.SetDestination(_waypoints[_currentWaypoint].position);
    }

    void Update()
    {
        if (_agent.remainingDistance < 0.5f && _currentWaypoint < _waypoints.Count - 1)
        {
            _currentWaypoint++;
            _agent.SetDestination(_waypoints[_currentWaypoint].position);
        }
    }
}
