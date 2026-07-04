using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left mouse button clicked");

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray rayOrigin = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, 100))
            {
                //print("Hit: " + hitInfo.transform.name);

                MeshRenderer hitObject = hitInfo.collider.GetComponent<MeshRenderer>();

                if(hitObject != null)
                {
                    switch (hitObject.transform.tag)
                    {
                        case "Cube":
                            hitObject.material.color = Random.ColorHSV();
                            break;

                        case "Capsule":
                            hitObject.material.color = Color.black;
                            break;
                    }
                }
                
            }
                
        }
    }
}
