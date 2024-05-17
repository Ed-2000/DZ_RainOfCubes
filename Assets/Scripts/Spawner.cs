using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T SpawnedObject;

    protected int MaxSize = 15;
    protected ObjectPool<T> Pool;
    protected Vector3 SpawnPosition;

    protected virtual void ActionOnGet(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(true);
    }

    protected virtual void ActionOnRelease(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
    }

    protected virtual void ActionOnDestroy(T spawnedObject)
    {
        Destroy(spawnedObject.gameObject);
    }

    protected virtual void Release(T spawnedObject)
    {
        Pool.Release(spawnedObject);
    }
}