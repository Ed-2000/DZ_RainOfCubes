using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CubeCollision))]
public class CubeVisualization : MonoBehaviour
{
    private CubeCollision _cubeCollision;

    private void OnEnable()
    {
        _cubeCollision = GetComponent<CubeCollision>();
        _cubeCollision.TouchedPlatform += SetRandomColor;
    }

    private void OnDisable()
    {
        _cubeCollision.TouchedPlatform -= SetRandomColor;
    }

    private void SetRandomColor()
    {
        float hueMin = 0;
        float hueMax = 1;
        float saturationMin = 1;
        float saturationMax = 1;
        float valueMin = 0.75f;
        float valueMax = 1;

        GetComponent<Renderer>().material.color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
    }
}