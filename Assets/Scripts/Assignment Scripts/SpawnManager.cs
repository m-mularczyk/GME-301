using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance { get; private set; }

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private float _spawnTimeStep = 6f;

    [SerializeField] private int _enemyPoolSize = 10;
    private List<GameObject> _enemyPool = new List<GameObject>();
    [SerializeField] private int _maxEnemiesToSpawn = 20;
    private int _enemiesLeftToSpawn;
    private int _enemiesKilled = 0;

    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < _enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, transform);
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }

    }
    void Start()
    {
        _enemiesLeftToSpawn = _maxEnemiesToSpawn;
        StartCoroutine(EnemySpawningRoutine());
        _uiManager.UpdateEnemyCount(_maxEnemiesToSpawn);
    }

    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        //Instantiate(_enemyPrefab, _spawnLocation.position, Quaternion.identity);
        //Debug.Log("Spawned enemy");
        GetEnemyFromPool(_spawnLocation.position, Quaternion.identity);
        //Debug.Log("Spawned enemy from pool");
    }

    IEnumerator EnemySpawningRoutine()
    {
        while (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_spawnTimeStep);
            SpawnEnemy();
            _enemiesLeftToSpawn--;
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

    public void EnemyKilled()
    {
        _enemiesKilled++;
        _uiManager.UpdateEnemyCount(_maxEnemiesToSpawn - _enemiesKilled);
        _uiManager.UpdateScore();
    }
}
