using System.Collections;
using UnityEngine;

public class EnergyBarrier : MonoBehaviour
{
    private int _barrierMaxLifes = 2;
    private int _timeForRecovery = 5;
    private int _currentBarrierLifes;

    private Collider _barrierCollider;
    private MeshRenderer _meshRenderer;

    void Start()
    {
        _barrierCollider = GetComponent<Collider>();
        if( _barrierCollider == null)
        {
            Debug.LogError("Barrier Collider not found");
        }

        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer == null)
        {
            Debug.LogError("Mesh Renderer not found");
        }

        _currentBarrierLifes = _barrierMaxLifes;
    }

    public void BarrierShot()
    {
        _currentBarrierLifes--;
        if(_currentBarrierLifes <= 0)
        {
            //Disable barrier
            _barrierCollider.enabled = false;
            _meshRenderer.enabled = false;

            //Start counter for revival
            StartCoroutine(BarrierRevivalRoutine());
        }
    }

    private void BarrierRevival()
    {
        _currentBarrierLifes = _barrierMaxLifes;
        _barrierCollider.enabled = true;
        _meshRenderer.enabled = true;
    }

    IEnumerator BarrierRevivalRoutine()
    {
        yield return new WaitForSeconds(_timeForRecovery);
        BarrierRevival();
    }
}
