using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CubeSpawnedZone : MonoBehaviour
{
    private float _minSpawnPositionX;
    private float _maxSpawnPositionX;
    private float _minSpawnPositionY;
    private float _maxSpawnPositionY;
    private float _minSpawnPositionZ;
    private float _maxSpawnPositionZ;

    private void Awake()
    {
        InitialLimitsOfStartingPosition();
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3();

        position.x = Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
        position.y = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
        position.z = Random.Range(_minSpawnPositionZ, _maxSpawnPositionZ);

        return position;
    }

    private void InitialLimitsOfStartingPosition()
    {
        int variableToSubtractHalf = 2;
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;

        float scaleX = scale.x / variableToSubtractHalf;
        float scaleY = scale.y / variableToSubtractHalf;
        float scaleZ = scale.z / variableToSubtractHalf;

        _minSpawnPositionX = -scaleX + position.x;
        _maxSpawnPositionX = scaleX + position.x;
        _minSpawnPositionY = -scaleY + position.y;
        _maxSpawnPositionY = scaleY + position.y;
        _minSpawnPositionZ = -scaleZ + position.z;
        _maxSpawnPositionZ = scaleZ + position.z;
    }
}