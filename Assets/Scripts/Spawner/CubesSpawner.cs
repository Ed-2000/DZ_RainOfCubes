using System;
using UnityEngine;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] private CubeSpawnedZone _cubeSpawnedZone;

    private float _startSpawnTime = 0.0f;
    private float _spawnRepeatRate = 0.1f;

    public event Action<int, int> ChangedCountsOfObjects;

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), _startSpawnTime, _spawnRepeatRate);
    }

    protected override Cube CreateFunc()
    {
        Cube cube = Instantiate(SpawnedObject);
        cube.transform.SetParent(Parent);
        CubeLifeCycle cubeLifeCycle = cube.GetComponent<CubeLifeCycle>();
        cubeLifeCycle.ReleaseToPoolCube += Release;

        CountOfCreatedObjects++;
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);

        return cube;
    }

    protected override void ActionOnRelease(Cube cube)
    {
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);
        base.ActionOnRelease(cube);
    }

    protected override void ActionOnDestroy(Cube cube)
    {
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);

        cube.GetComponent<CubeLifeCycle>().ReleaseToPoolCube -= Release;
        Destroy(cube.gameObject);
    }

    protected void GetCube()
    {
        Cube cube = Pool.Get();
        ChangedCountsOfObjects?.Invoke(CountOfCreatedObjects, Pool.CountActive);
        cube.transform.position = _cubeSpawnedZone.GetRandomPosition();
    }
}