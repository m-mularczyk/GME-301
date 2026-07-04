using Unity.VectorGraphics;
using UnityEngine;

public class Player4 : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0f));
    }
}
