using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T SpawnedObject;
    [SerializeField] protected Transform Parent;

    protected int CountOfCreatedObjects = 0;
    protected ObjectPool<T> Pool;
    protected Vector3 SpawnPosition;

    private void Awake()
    {
        Pool = new ObjectPool<T>
            (
            createFunc: () => CreateFunc(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj)
            );
    }

    protected abstract T CreateFunc();

    protected virtual void ActionOnGet(T spawnedObject)
    {
        spawnedObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        spawnedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        spawnedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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