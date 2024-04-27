using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Platforma _platform;

    private float _startSpawnTime = 0;
    private float _spawnRepeatRate = 0.15f;
    private Vector3 _spawnPosition;
    private float _minSpawnPositionX;
    private float _maxSpawnPositionX;
    private float _minSpawnPositionZ;
    private float _maxSpawnPositionZ;

    private void Start()
    {
        InitialLimitsOfStartingPosition();
        InvokeRepeating(nameof(Spawn), _startSpawnTime, _spawnRepeatRate);
    }

    private void Spawn()
    {
        _spawnPosition.x = Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
        _spawnPosition.z = Random.Range(_minSpawnPositionZ, _maxSpawnPositionZ);

        Instantiate(_cubePrefab, _spawnPosition, Quaternion.identity);
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