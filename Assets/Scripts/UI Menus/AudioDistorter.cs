using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDistorter : MonoBehaviour
{
    private AudioSource source;

    [Header("Configuración de Variación")]
    [Range(0.1f, 2f)] public float speed = 0.5f; // Velocidad de la oscilación

    [Header("Límites de Pitch")]
    public float minPitch = 0.5f; // Recomendado no bajar de 0.1 para evitar errores
    public float maxPitch = 1.5f;

    [Header("Límites de Reverb (Mix)")]
    [Range(0f, 1f)] public float minReverb = 0.2f;
    public float maxReverb = 0.8f;

    private float seedPitch;
    private float seedPan;
    private float seedReverb;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        
        // Semillas para que el Perlin Noise sea diferente en cada eje
        seedPitch = Random.value * 100f;
        seedPan = Random.value * 100f;
        seedReverb = Random.value * 100f;
    }

    void Update()
    {
        float t = Time.time * speed;

        // 1. Variar Pitch
        float noisePitch = Mathf.PerlinNoise(t, seedPitch);
        source.pitch = Mathf.Lerp(minPitch, maxPitch, noisePitch);

        // 2. Variar Stereo Pan
        float noisePan = Mathf.PerlinNoise(t, seedPan);
        source.panStereo = Mathf.Lerp(-0.5f, 0.5f, noisePan);

        // 3. Variar Reverb Zone Mix
        float noiseReverb = Mathf.PerlinNoise(t, seedReverb);
        source.reverbZoneMix = Mathf.Lerp(minReverb, maxReverb, noiseReverb);
    }
}