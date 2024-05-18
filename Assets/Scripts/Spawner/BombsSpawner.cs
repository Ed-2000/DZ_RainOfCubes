using System;
using UnityEngine;

public class BombsSpawner : Spawner<Bomb>
{
    public event Action<int, int> ChangedCountsOfObjects;

    protected override Bomb CreateFunc()
    {
        Bomb bomb = Instantiate(SpawnedObject);
        bomb.transform.SetParent(Parent);
        bomb.HasExploded += Release;
        CountOfCreatedObjects++;
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);

        return bomb;
    }

    protected override void ActionOnRelease(Bomb bomb)
    {
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);
        base.ActionOnRelease(bomb);
    }

    protected override void ActionOnDestroy(Bomb bomb)
    {
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);

        bomb.HasExploded -= Release;
        Destroy(bomb.gameObject);
    }

    public void GetBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);
        bomb.transform.position = position;
    }
}