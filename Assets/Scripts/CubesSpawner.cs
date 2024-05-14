using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Platforma _platform;

    private CubeLifeCycle _cubeLifeCycle;
    private ObjectPool<Cube> _pool;
    private Vector3 _spawnPosition;
    private float _startSpawnTime = 0.0f;
    private float _spawnRepeatRate = 0.15f;
    private float _minSpawnPositionX;
    private float _maxSpawnPositionX;
    private float _minSpawnPositionZ;
    private float _maxSpawnPositionZ;

    private void Start()
    {
        InitialLimitsOfStartingPosition();

        _pool = new ObjectPool<Cube>
            (
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj)
            );

        InvokeRepeating(nameof(GetCube), _startSpawnTime, _spawnRepeatRate);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.GetComponent<CubeLifeCycle>().ReleaseToPoolCube += Release;

        _spawnPosition.x = Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
        _spawnPosition.z = Random.Range(_minSpawnPositionZ, _maxSpawnPositionZ);

        cube.transform.position = _spawnPosition;
        cube.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.GetComponent<CubeLifeCycle>().ReleaseToPoolCube -= Release;

        cube.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Cube cube)
    {
        cube.GetComponent<CubeLifeCycle>().ReleaseToPoolCube -= Release;

        Destroy(cube.gameObject);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void Release(Cube cube)
    {
        _pool.Release(cube);
    }

    private void InitialLimitsOfStartingPosition()
    {
        int height = 5;
        int variableToSubtractHalf = 2;
        float indentationFromEdgeOfPlatform = _cubePrefab.transform.localScale.x / variableToSubtractHalf;
        Transform platformTransform = _platform.transform;

        _spawnPosition.y = _platform.transform.position.y + height;
        _minSpawnPositionX = platformTransform.position.x - platformTransform.localScale.x / variableToSubtractHalf + indentationFromEdgeOfPlatform;
        _maxSpawnPositionX = platformTransform.position.x + platformTransform.localScale.x / variableToSubtractHalf - indentationFromEdgeOfPlatform;
        _minSpawnPositionZ = platformTransform.position.z - platformTransform.localScale.z / variableToSubtractHalf + indentationFromEdgeOfPlatform;
        _maxSpawnPositionZ = platformTransform.position.z + platformTransform.localScale.z / variableToSubtractHalf - indentationFromEdgeOfPlatform;
    }
}