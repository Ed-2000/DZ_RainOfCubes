using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T SpawnedObject;
    [SerializeField] protected int CountOfCreatedObjects = 0;
    [SerializeField] protected int CountOfActiveObjects = 0;

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