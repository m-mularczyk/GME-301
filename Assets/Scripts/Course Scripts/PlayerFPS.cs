using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFPS : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHolePrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //cast a ray from the center of the screen
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);

            Ray rayOrigin = Camera.main.ViewportPointToRay(screenCenter);
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
                // check where it hits
                Vector3 hitPosition = hitInfo.point;

                //instantiate prefab there
                Instantiate(_bulletHolePrefab, hitPosition, Quaternion.LookRotation(hitInfo.normal));
            }
        }
    }
}
