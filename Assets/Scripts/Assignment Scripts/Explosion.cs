using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //StartCoroutine("DestroyCoroutine");
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAI>()?.OnEnemyDead();
        }
    }

    IEnumerable DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        
    }
}
