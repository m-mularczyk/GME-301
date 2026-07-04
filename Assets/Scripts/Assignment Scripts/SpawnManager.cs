using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance { get; private set; }

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnLocation;

    [SerializeField] private bool _isSpawning= true;
    [SerializeField] private float _spawnTimeStep = 6f;

    [SerializeField] private int _enemyPoolSize = 10;
    private List<GameObject> _enemyPool = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);

        for (int i = 0; i < _enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, transform);
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }

    }
    void Start()
    {
        StartCoroutine(EnemySpawningRoutine());
    }

    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        //Instantiate(_enemyPrefab, _spawnLocation.position, Quaternion.identity);
        //Debug.Log("Spawned enemy");
        GetEnemyFromPool(_spawnLocation.position, Quaternion.identity);
        Debug.Log("Spawned enemy from pool");
    }

    IEnumerator EnemySpawningRoutine()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(_spawnTimeStep);
            SpawnEnemy();
        }
        
    }

    private GameObject GetEnemyFromPool(Vector3 position, Quaternion rotation)
    {
        // Get enemy object from pool if possible
        foreach (GameObject enemy in _enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.transform.rotation = rotation;
                enemy.SetActive(true);
                return enemy;
            }
        }

        // If pool is too small - create new enemy object and add to pool
        GameObject newEnemy = Instantiate(_enemyPrefab, position, rotation, transform);
        _enemyPool.Add(newEnemy);
        return newEnemy;
    }
}
