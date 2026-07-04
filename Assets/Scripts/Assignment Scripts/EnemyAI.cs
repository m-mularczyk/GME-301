using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private NavMeshAgent _agent;

    [SerializeField] private int _currentWaypoint = 0;
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();


    private void Awake()
    {
        var waypointsHolder = GameObject.Find("Enemy AI waypoints");
        for (int i = 0; i < waypointsHolder.transform.childCount; i++)
        {
            _waypoints.Add(waypointsHolder.transform.GetChild(i));
        }
        
        if(_waypoints.Count < 2)
        {
            Debug.LogError("Not enough waypoints for enemy AI Agent");
        }
    }
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

    private void OnEnable()
    {
        _currentWaypoint = 0;

        if (_agent != null && _waypoints != null && _waypoints.Count > 0)
        {
            _agent.ResetPath();
            _agent.SetDestination(_waypoints[0].position);
        }
    }

    void Update()
    {
        if (_agent.remainingDistance < 0.5f && _currentWaypoint < _waypoints.Count - 1)
        {
            _currentWaypoint++;
            _agent.SetDestination(_waypoints[_currentWaypoint].position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyEndPosition"))
        {
            gameObject.SetActive(false);
        }
    }

    /*
    private void OnDisable()
    {
        _currentWaypoint = 0;

        if (_agent != null)
            _agent.ResetPath();
    }
    */
}
