using UnityEngine;
using UnityEngine.InputSystem;

public class Player3 : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayerMask;

    void Start()
    {
        
    }

    void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            /*
            if(Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 6))
            {
                hitInfo.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            */

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, _enemyLayerMask))
            {
                hitInfo.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        
    }
}
