using UnityEngine;

public class CubesSpawnerUI : SpawnerUI
{
    [SerializeField] private CubesSpawner _spawner;

    private void OnEnable()
    {
        _spawner.ChangedCountsOfObjects += Draw;
    }

    private void OnDisable()
    {
        _spawner.ChangedCountsOfObjects -= Draw;
    }
}
