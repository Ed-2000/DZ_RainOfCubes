using UnityEngine;

public class BombsSpawnerUI : SpawnerUI
{
    [SerializeField] private BombsSpawner _spawner;

    private void OnEnable()
    {
        _spawner.ChangedCountsOfObjects += Draw;
    }

    private void OnDisable()
    {
        _spawner.ChangedCountsOfObjects -= Draw;
    }
}
