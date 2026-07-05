using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private float _maxTimeInSeconds = 120f;

    [SerializeField] private int _scorePerEnemy = 50;

    [SerializeField] private int _score = 0;
    [SerializeField] private int _enemyCount;

    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _enemyCountTxt;
    [SerializeField] private TextMeshProUGUI _timeLeftTxt;

    private float _timeDeadline;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _scoreTxt.text = "Score: 0";
        _timeDeadline = Time.time + _maxTimeInSeconds;
    }

    void Update()
    {
        float timeLeft = _timeDeadline - Time.time;

        if (timeLeft < 0f)
            timeLeft = 0f;

        UpdateTimeUI(timeLeft);
    }

    public void UpdateScore()
    {
        _score += _scorePerEnemy;
        _scoreTxt.text = "Score: " + _score;
    }

    public void UpdateEnemyCount(int enemiesLeft)
    {
        _enemyCount = enemiesLeft;
        _enemyCountTxt.text = "Enemies: " + _enemyCount;
    }

    private void UpdateTimeUI(float timeLeft)
    {
        _timeLeftTxt.text = "Time: " + Mathf.CeilToInt(timeLeft);
    }
}
