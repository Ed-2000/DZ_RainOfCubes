using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CubeCollision : MonoBehaviour
{
    private bool _isFirstCollisionWithPlatforma = true;

    public event Action TouchedPlatform;

    private void OnDisable()
    {
        _isFirstCollisionWithPlatforma = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Platforma>() != null)
        {
            if (_isFirstCollisionWithPlatforma)
            {
                TouchedPlatform?.Invoke();
                _isFirstCollisionWithPlatforma = false;
            }
        }
    }
}
