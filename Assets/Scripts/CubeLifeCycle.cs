using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeCollision))]
public class CubeLifeCycle : MonoBehaviour
{
    private CubeCollision _cubeCollision;
    private int _lifetime;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    private void OnEnable()
    {
        _cubeCollision = GetComponent<CubeCollision>();
        _cubeCollision.TouchedPlatform += DestroyWithDelayStarter;
    }

    private void OnDisable()
    {
        _cubeCollision.TouchedPlatform -= DestroyWithDelayStarter;
    }

    private void DestroyWithDelayStarter()
    {
        _lifetime = Random.Range(_minLifetime, _maxLifetime);
        StartCoroutine(DestroyWithDelay(_lifetime));
    }

    private IEnumerator DestroyWithDelay(int delay)
    {
        var wait = new WaitForSeconds(1);

        for (int i = delay; i > 0; i--)
            yield return wait;

        Destroy(gameObject);
    }
}