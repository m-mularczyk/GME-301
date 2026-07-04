using UnityEngine;
using UnityEngine.InputSystem;

public class PointerScript : MonoBehaviour
{
    [SerializeField] private Player5 _player;

    void Start()
    {
        _player = FindAnyObjectByType<Player5>();
        if( _player == null)
        {
            Debug.LogError("Unable to find Player5");
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            // if raycast hit anything
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if(hitInfo.collider.tag == "Floor")
                {
                    //set destination
                    Vector3 newDestination = hitInfo.point;
                    _player.SetNewDestination(newDestination);
                } 
            }
        }
    }
}
