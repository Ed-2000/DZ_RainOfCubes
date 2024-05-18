using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    private float _explosionForce = 300.0f;
    private float _radius = 15.0f;
    private int _lifetime;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private Material baseMaterial;
    private Material material;

    public event Action<Bomb> HasExplosion;

    private void Awake()
    {
        material = gameObject.GetComponent<Renderer>().material;
        baseMaterial = material;
    }

    private void OnEnable()
    {
        _lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        StartCoroutine(DisappearAndExplosion());
    }

    private void Explosion()
    {
        foreach (var explosionObject in GetExplosionObjects())
            explosionObject.AddExplosionForce(_explosionForce, transform.position, _radius);

        HasExplosion(gameObject.GetComponent<Bomb>());
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

    private IEnumerator DisappearAndExplosion()
    {
        var wait = new WaitForEndOfFrame();
        float currentTime = 0.0f;
        Color color = baseMaterial.color;
        float speed = 1.0f / _lifetime;

        while (currentTime <= _lifetime)
        {
            currentTime += Time.deltaTime;

            color.a = Mathf.MoveTowards(color.a, 0, speed * Time.deltaTime);
            material.color = color;

            yield return wait;
        }

        Explosion();
    }
}