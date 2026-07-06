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

    private UIManager _uiManager;
    private Animator _animator;
    private Coroutine _hidingRoutine;
    private AudioManager _audioManager;
    private bool _isDead = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("NavMeshAgent not found!");
        }
        _uiManager = FindAnyObjectByType<UIManager>();
        if( _uiManager == null)
        {
            Debug.LogError("UIManager not found!");
        }

        _audioManager = FindAnyObjectByType<AudioManager>();
        if( _audioManager == null)
        {
            Debug.LogError("Audio Manager not found");
        }

        //_animator = GetComponentInChildren<Animator>();
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator not found!");
        }

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
        _hidingRoutine = StartCoroutine(EnemyHidingRoutine());
    }

    private void OnEnable()
    {
        _isDead = false;

        _enemyAIState = EnemyAIState.Run;
        _currentWaypoint = 0;
        transform.GetComponent<Collider>().enabled = true;

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
                //Debug.Log("Running...");
                _agent.isStopped = false;
                _animator.SetBool("Hiding", false);
                _animator.SetFloat("Speed", 10f);
                break;
            case EnemyAIState.Hide:
                //Debug.Log("Hiding...");
                _agent.isStopped = true;
                _animator.SetBool("Hiding", true);
                break;
            case EnemyAIState.Death:
                //Debug.Log("Dead");
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
            transform.parent.GetComponent<SpawnManager>()?.EnemyEscaped();
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("EnemyHidingPoint"))
        {
            _enemyAIState = EnemyAIState.Hide;
            _hidingRoutine = StartCoroutine(EnemyHidingRoutine());
        }
    }

    IEnumerator EnemyHidingRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 4f));

        if (_enemyAIState == EnemyAIState.Death)
            yield break;

        _enemyAIState = EnemyAIState.Run;
    }

    public void OnEnemyDead()
    {
        if(_isDead) return;
        
        _isDead = true;

        _animator.SetTrigger("Death");

        _enemyAIState = EnemyAIState.Death;

        _agent.isStopped = true;
        transform.GetComponent<Collider>().enabled = false; // Preventing killing the same enemy multiple times

        if (_hidingRoutine != null)
        {
            StopCoroutine(_hidingRoutine);
            _hidingRoutine = null;
        }

        transform.parent.GetComponent<SpawnManager>()?.EnemyKilled();
        StartCoroutine(EnemyDeathRoutine());
        _audioManager.PlayEnemyDyingSound(transform.position);
        //Debug.Break();
    }

    IEnumerator EnemyDeathRoutine()
    {
        Debug.Log("Enemy killed");
        yield return new WaitForSeconds(3.5f); // Waiting for death animation to play
        gameObject.SetActive(false);
    }
}
