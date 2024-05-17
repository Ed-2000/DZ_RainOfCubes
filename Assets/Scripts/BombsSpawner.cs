using UnityEngine;
using UnityEngine.Pool;

public class BombsSpawner : Spawner<Bomb>
{
    private void Awake()
    {
        Pool = new ObjectPool<Bomb>
            (
            createFunc: () => Instantiate(SpawnedObject),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            maxSize: MaxSize
            );
    }

    public void GetBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
    }
}