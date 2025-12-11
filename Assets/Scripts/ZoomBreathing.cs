using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomBreathing : MonoBehaviour
{
    public Camera cam;

    [Header("Breathing Settings")]
    public float baseFOV = 60f;          // FOV normal
    public float amplitude = 2f;         // cuánto se aleja o acerca (±)
    public float speed = 1.5f;           // velocidad del pulso
    public bool autoStart = true;        // si comienza a respirar automáticamente

    bool active = false;
    float timeCounter = 0f;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        if (autoStart)
            active = true;
    }

    void Update()
    {
        if (!active) return;

        timeCounter += Time.deltaTime * speed;

        float offset = Mathf.Sin(timeCounter) * amplitude;
        cam.fieldOfView = baseFOV + offset;
    }

    // Permite encender la respiración desde otro script
    public void EnableBreathing()
    {
        active = true;
    }

    // Permite apagarla desde otro script
    public void DisableBreathing()
    {
        active = false;
        cam.fieldOfView = baseFOV; // Resetea el FOV
    }
}