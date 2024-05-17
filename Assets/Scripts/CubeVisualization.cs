using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CubeCollision))]
public class CubeVisualization : MonoBehaviour
{
    private CubeCollision _cubeCollision;
    private CubeLifeCycle _ñubeLifeCycle;
    private Renderer _renderer;
    private Color _baseColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _cubeCollision = GetComponent<CubeCollision>();
        _ñubeLifeCycle = GetComponent<CubeLifeCycle>();

        _baseColor = _renderer.material.color;

        _cubeCollision.TouchedPlatform += SetRandomColor;
        _ñubeLifeCycle.ReleaseToPoolCube += SetBaseColor;
    }

    private void OnDestroy()
    {
        _cubeCollision.TouchedPlatform -= SetRandomColor;
        _ñubeLifeCycle.ReleaseToPoolCube -= SetBaseColor;
    }

    private void SetRandomColor()
    {
        float hueMin = 0;
        float hueMax = 1;
        float saturationMin = 1;
        float saturationMax = 1;
        float valueMin = 0.75f;
        float valueMax = 1;

        _renderer.material.color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
    }

    private void SetBaseColor(Cube cube)
    {
        _renderer.material.color = _baseColor;
    }
}