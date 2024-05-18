using System;
using UnityEngine;
using UnityEngine.Pool;

public class BombsSpawner : Spawner<Bomb>
{
    public event Action<int, int> ChangedCountsOfObjects;

    private void Awake()
    {
        Pool = new ObjectPool<Bomb>
            (
            createFunc: () => CreateFunc(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj)
            );
    }

    private Bomb CreateFunc()
    {
        Bomb bomb = Instantiate(SpawnedObject);
        bomb.HasExplosion += Release;
        CountOfCreatedObjects++;
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);

        return bomb;
    }

    public void GetBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();

        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);
        bomb.transform.position = position;
    }
    protected override void ActionOnRelease(Bomb bomb)
    {
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);
        base.ActionOnRelease(bomb);
    }

    protected override void ActionOnDestroy(Bomb bomb)
    {
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);

        bomb.HasExplosion -= Release;
        Destroy(bomb.gameObject);
    }
}