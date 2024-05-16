using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeCollision), typeof(Cube))]
public class CubeLifeCycle : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefabe;
    [SerializeField] private Spawner<Bomb> _spawner;

    private CubeCollision _cubeCollision;
    private int _lifetime;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    public event Action<Cube> ReleaseToPoolCube;

    private void OnEnable()
    {
        _spawner = new Spawner<Bomb>();

        _cubeCollision = GetComponent<CubeCollision>();
        _cubeCollision.TouchedPlatform += ReleaseToPoolWithDelayStarter;
    }

    private void OnDisable()
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

        SpawnBomb();
        ReleaseToPoolCube?.Invoke(gameObject.GetComponent<Cube>());
    }

    private void SpawnBomb()
    {
        _spawner.Pool.Get();
        Instantiate(_bombPrefabe, transform.position, Quaternion.identity);
    }
}