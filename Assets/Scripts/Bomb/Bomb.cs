using System;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 450.0f;
    [SerializeField] private float _radius = 15.0f;
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private int _lifetime;

    public event Action<Bomb> HasExploded;
    public event Action<float> HasEnabled;

    private void OnEnable()
    {
        _lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);

        HasEnabled?.Invoke(_lifetime);
        Invoke(nameof(Explode), _lifetime);
    }

    private void Explode()
    {
        foreach (var explosionObject in GetExplosionObjects())
            explosionObject.AddExplosionForce(_explosionForce, transform.position, _radius);

        HasExploded?.Invoke(this);
    }

    private List<Rigidbody> GetExplosionObjects()
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbodies.Add(rigidbody);
        }

        return rigidbodies;
    }
}