using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] private Platform _platform;
    [SerializeField] private BombsSpawner _bombsSpawner;

    private CubeLifeCycle _cubeLifeCycle;
    private Vector3 _spawnPosition;
    private float _startSpawnTime = 0.0f;
    private float _spawnRepeatRate = 0.35f;
    private float _minSpawnPositionX;
    private float _maxSpawnPositionX;
    private float _minSpawnPositionZ;
    private float _maxSpawnPositionZ;

    private void Awake()
    {
        InitialLimitsOfStartingPosition();

        Pool = new ObjectPool<Cube>
            (
            createFunc: () => CreateFunc(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj)
            );

        InvokeRepeating(nameof(GetCube), _startSpawnTime, _spawnRepeatRate);
    }

    private Cube CreateFunc()
    {
        Cube cube = Instantiate(SpawnedObject);
        CubeLifeCycle cubeLifeCycle = cube.GetComponent<CubeLifeCycle>();
        cubeLifeCycle.ReleaseToPoolCube += Release;
        cubeLifeCycle.BombsSpawner = _bombsSpawner;
        CountOfCreatedObjects++;

        return cube;
    }

    protected override void ActionOnGet(Cube cube)
    {
        CountOfActiveObjects = Pool.CountActive;

        _spawnPosition.x = UnityEngine.Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
        _spawnPosition.z = UnityEngine.Random.Range(_minSpawnPositionZ, _maxSpawnPositionZ);

        cube.transform.position = _spawnPosition;
        cube.gameObject.SetActive(true);
    }

    protected override void ActionOnDestroy(Cube cube)
    {
        cube.GetComponent<CubeLifeCycle>().ReleaseToPoolCube -= Release;
        Destroy(cube.gameObject);
    }

    protected void GetCube()
    {
        Pool.Get();
    }

    private void InitialLimitsOfStartingPosition()
    {
        int height = 5;
        int variableToSubtractHalf = 2;
        float indentationFromEdgeOfPlatform = SpawnedObject.transform.localScale.x / variableToSubtractHalf;
        Transform platformTransform = _platform.transform;

        _spawnPosition.y = _platform.transform.position.y + height;
        _minSpawnPositionX = platformTransform.position.x - platformTransform.localScale.x / variableToSubtractHalf + indentationFromEdgeOfPlatform;
        _maxSpawnPositionX = platformTransform.position.x + platformTransform.localScale.x / variableToSubtractHalf - indentationFromEdgeOfPlatform;
        _minSpawnPositionZ = platformTransform.position.z - platformTransform.localScale.z / variableToSubtractHalf + indentationFromEdgeOfPlatform;
        _maxSpawnPositionZ = platformTransform.position.z + platformTransform.localScale.z / variableToSubtractHalf - indentationFromEdgeOfPlatform;
    }
}