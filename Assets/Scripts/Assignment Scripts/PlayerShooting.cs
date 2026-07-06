using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);

            Ray rayOrigin = Camera.main.ViewportPointToRay(screenCenter);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 10))
            {
                Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    hitInfo.collider.GetComponent<EnemyAI>()?.OnEnemyDead();
                }
                else if (hitInfo.collider.CompareTag("Barrier"))
                {
                    _audioManager.PlayBarrierHitSound();
                }
                else if (hitInfo.collider.CompareTag("ExplosiveBarrel"))
                {
                    hitInfo.collider.GetComponent<ExplosiveBarrel>()?.Explode();
                    _audioManager.PlayExplosionSound();
                }
            }

            _audioManager.PlayShotSound();
        }
    }
}
