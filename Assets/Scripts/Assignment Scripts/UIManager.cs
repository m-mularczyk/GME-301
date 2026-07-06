using GameDevHQ.FileBase.Plugins.FPS_Character_Controller;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TextMeshProUGUI _gameOverTxt;
    [SerializeField] private TextMeshProUGUI _successTxt;

    [SerializeField] private FPS_Controller _playerController;

    private float _timeDeadline;
    private bool _timerIsCounting = true;

    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _scoreTxt.text = "Score: 0";
        _timeDeadline = Time.time + _maxTimeInSeconds;
        _timerIsCounting = true;
        _gameOverTxt.gameObject.SetActive(false);
        _successTxt.gameObject.SetActive(false);

        _playerController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (_timerIsCounting)
        {
            float timeLeft = _timeDeadline - Time.time;

            if (timeLeft < 0f)
            {
                timeLeft = 0f;
                GameOver("Time is up");
            }
                


            UpdateTimeUI(timeLeft);
        }
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

    public void GameOver(string information)
    {
        _playerController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _gameOverTxt.gameObject.SetActive(true);
        _gameOverTxt.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = information;
        _timerIsCounting = false;
        _audioManager.PlayGameOverSound();
        Time.timeScale = 0;
    }

    public void Success()
    {
        _playerController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _successTxt.gameObject.SetActive(true);
        _timerIsCounting = false;
        _audioManager.PlaySuccessSound();
        Time.timeScale = 0;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
