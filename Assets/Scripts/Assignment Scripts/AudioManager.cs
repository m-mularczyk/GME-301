using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _dyingEnemySound;
    [SerializeField] private AudioClip _successSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _barrierHitSound;
    [SerializeField] private AudioClip _explosionSound;

    private AudioSource _audioPlayer;
    private bool _isGameOver = false;
    private bool _isGameFinished = false;

    void Start()
    {
        _audioPlayer = GetComponent<AudioSource>();
        if( _audioPlayer == null)
        {
            Debug.LogError("AudioPlayer is null");
        }
    }

    public void PlayShotSound()
    {
        if (_isGameOver != true && _isGameFinished != true)
        {
            _audioPlayer.PlayOneShot(_shotSound);
        }
    }

    public void PlayBarrierHitSound()
    {
        if (_isGameOver != true && _isGameFinished != true)
        {
            _audioPlayer.PlayOneShot(_barrierHitSound, 1.5f);
        }
    }

    public void PlaySuccessSound()
    {
        _audioPlayer.PlayOneShot(_successSound);
        _isGameFinished = true;
    }

    public void PlayGameOverSound()
    {
        if (!_isGameOver)
        {
            _audioPlayer.PlayOneShot(_gameOverSound);
        }
        _isGameOver = true;
    }

    public void PlayEnemyDyingSound(Vector3 enemyPosition)
    {
        //AudioSource.PlayClipAtPoint(_dyingEnemySound, enemyPosition, 5f);
        _audioPlayer.PlayOneShot(_dyingEnemySound, 0.2f);
    }

    public void PlayExplosionSound()
    {
        _audioPlayer.PlayOneShot(_explosionSound, 0.5f);
    }
}
