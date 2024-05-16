using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _spawnedObject;

    public ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>
            (
            createFunc: () => Instantiate(_spawnedObject),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj)
            );
    }

    private void ActionOnGet(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(true);
    }

    private void ActionOnRelease(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(T spawnedObject)
    {
        Destroy(spawnedObject.gameObject);
    }
}