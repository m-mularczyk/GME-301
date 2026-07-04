using UnityEngine;

public class Sphere : MonoBehaviour
{

    private Rigidbody _rigidBody;
 
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        RaycastHit hitInfo;

        
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 2f))
        {
            Debug.DrawRay(transform.position, Vector3.down * 2, Color.blue);
            _rigidBody.useGravity = false;
            _rigidBody.isKinematic = true;
             Debug.Log("Almost on the ground");
        }

    }

}
