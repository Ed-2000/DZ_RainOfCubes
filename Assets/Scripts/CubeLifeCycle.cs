using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeCollision), typeof(Cube))]
public class CubeLifeCycle : MonoBehaviour
{
    private BombsSpawner _bombsSpawner;
    private CubeCollision _cubeCollision;
    private int _lifetime;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    public BombsSpawner BombsSpawner { get => _bombsSpawner; set => _bombsSpawner = value; }

    public event Action<Cube> ReleaseToPoolCube;

    private void Awake()
    {
        _cubeCollision = GetComponent<CubeCollision>();
        _cubeCollision.TouchedPlatform += ReleaseToPoolWithDelayStarter;
    }

    private void OnDestroy()
    {
        _cubeCollision.TouchedPlatform -= ReleaseToPoolWithDelayStarter;
    }

    private void ReleaseToPoolWithDelayStarter()
    {
        _lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        StartCoroutine(ReleaseToPoolWithDelay(_lifetime));
    }

    private IEnumerator ReleaseToPoolWithDelay(int delay)
    {
        var wait = new WaitForSeconds(1);

        for (int i = delay; i > 0; i--)
            yield return wait;

        BombsSpawner.GetBomb(transform.position);
        ReleaseToPoolCube?.Invoke(gameObject.GetComponent<Cube>());
    }
}