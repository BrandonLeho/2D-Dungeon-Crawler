using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObject : MonoBehaviour
{
    public float initialDuration = 10f;
    public float blinkStart = 5f;
    public float minOpacity = 0.2f;
    public float maxOpacity = 1f;
    public float minBlinkSpeed = 0.1f;
    public float maxBlinkSpeed = 5f;

    private float timer;
    private Renderer rend;

    void Start()
    {
        timer = initialDuration;
        rend = GetComponent<Renderer>();
        SetOpacity(maxOpacity);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else if (timer <= blinkStart)
        {
            // Adjust opacity
            float targetOpacity = Mathf.Lerp(minOpacity, maxOpacity, Mathf.PingPong(Time.time * CalculateBlinkSpeed(), 1f));
            SetOpacity(targetOpacity);
        }
    }

    float CalculateBlinkSpeed()
    {
        float timeRatio = 1f - (timer / blinkStart);
        return Mathf.Lerp(minBlinkSpeed, maxBlinkSpeed, timeRatio);
    }

    void SetOpacity(float opacity)
    {
        Color color = rend.material.color;
        color.a = opacity;
        rend.material.color = color;
    }
}
