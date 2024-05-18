using UnityEngine;
using UnityEngine.Pool;

public class BombsSpawner : Spawner<Bomb>
{
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

        return bomb;
    }

    public void GetBomb(Vector3 position)
    {
        CountOfActiveObjects = Pool.CountActive;
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
    }

    protected override void ActionOnDestroy(Bomb bomb)
    {
        bomb.HasExplosion -= Release;
        Destroy(bomb.gameObject);
    }
}