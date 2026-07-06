using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Collider _explosionRadiusCollider;

    void Start()
    {
        
    }

    public void Explode()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Debug.Log("Barrel exploded");
        _explosionRadiusCollider.gameObject.SetActive(true);
        _explosionRadiusCollider.transform.SetParent(null);
        Destroy(gameObject);

    }


}
