using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CubeCollision : MonoBehaviour
{
    private bool _isFirstCollisionWithPlatform = true;

    public event Action TouchedPlatform;

    private void OnDisable()
    {
        _isFirstCollisionWithPlatform = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Platform _))
        {
            if (_isFirstCollisionWithPlatform)
            {
                TouchedPlatform?.Invoke();
                _isFirstCollisionWithPlatform = false;
            }
        }
    }
}
