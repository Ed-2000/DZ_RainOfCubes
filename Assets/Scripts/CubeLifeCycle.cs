using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeCollision), typeof(Cube))]
public class CubeLifeCycle : MonoBehaviour
{
    private CubeCollision _cubeCollision;
    private int _lifetime;
    private int _minLifetime = 1;
    private int _maxLifetime = 1;

    public event Action<Cube> ReleaseToPoolCube;

    private void OnEnable()
    {
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

        ReleaseToPoolCube?.Invoke(gameObject.GetComponent<Cube>());
    }
}