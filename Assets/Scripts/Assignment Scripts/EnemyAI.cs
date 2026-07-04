using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private enum EnemyAIState
    {
        Run,
        Hide,
        Death
    }

    private NavMeshAgent _agent;

    [SerializeField] private EnemyAIState _enemyAIState = EnemyAIState.Run;

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
        switch (_enemyAIState)
        {
            case EnemyAIState.Run:
                // Intelligently select barriers to run and hide behind. 
                _agent.isStopped = false;
                Debug.Log("Running...");
                break;
            case EnemyAIState.Hide:
                // Stop running when they are at their selected barrier for a random amount of time.
                _agent.isStopped = true;
                Debug.Log("Hiding...");
                //StartCoroutine(EnemyHidingRoutine());
                break;
            case EnemyAIState.Death:
                // Triggered when enemy is shot by player
                // Award 50 points to player
                // Start dying animation
                Debug.Log("Dead");
                break;
        }


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
        else if (other.CompareTag("EnemyHidingPoint"))
        {
            _enemyAIState = EnemyAIState.Hide;
            StartCoroutine(EnemyHidingRoutine());
        }
    }

    IEnumerator EnemyHidingRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 4f));
        _enemyAIState = EnemyAIState.Run;
    }
}
