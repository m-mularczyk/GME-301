using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);

            Ray rayOrigin = Camera.main.ViewportPointToRay(screenCenter);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 10))
            {
                //Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    hitInfo.collider.GetComponent<EnemyAI>()?.OnEnemyDead();
                }
            }
        }
    }
}
