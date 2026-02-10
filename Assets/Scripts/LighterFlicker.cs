using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterFlicker : MonoBehaviour
{
    public Light flameLight;

    [Header("Intensity")]
    public float baseIntensity = 1.2f;
    public float intensityVariation = 0.2f;
    public float flickerSpeed = 2f;

    [Header("Color")]
    public Color colorA = new Color(1f, 0.75f, 0.3f); // amarillo cálido
    public Color colorB = new Color(1f, 0.6f, 0.15f); // amarillo más oscuro

    float noiseOffset;

    void Start()
    {
        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (flameLight == null) return;

        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, noiseOffset);

        // Intensidad suave
        flameLight.intensity = baseIntensity + (noise - 0.5f) * intensityVariation;

        // Color suave
        flameLight.color = Color.Lerp(colorA, colorB, noise);
    }
}