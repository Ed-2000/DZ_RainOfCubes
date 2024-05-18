using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Bomb))]
public class BombVisualization : MonoBehaviour
{
    private Bomb _bomb;
    private Renderer _renderer;
    private Color _baseColor;

    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
        _bomb.HasEnabled += DisapearStarter;
        _renderer = GetComponent<Renderer>();

        _baseColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        _renderer.material.color = _baseColor;
    }

    private void OnDestroy()
    {
        _bomb.HasEnabled -= DisapearStarter;
    }

    private void DisapearStarter(float lifetime)
    {
        StartCoroutine(Disappear(lifetime));
    }

    private IEnumerator Disappear(float lifetime)
    {
        var wait = new WaitForEndOfFrame();
        float currentTime = 0.0f;
        Color color = _renderer.material.color;
        float speed = 1.0f / lifetime;

        while (currentTime <= lifetime)
        {
            currentTime += Time.deltaTime;

            color.a = Mathf.MoveTowards(color.a, 0, speed * Time.deltaTime);
            _renderer.material.color = color;

            yield return wait;
        }
    }
}