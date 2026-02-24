using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepRaycastController : MonoBehaviour
{
    public AudioSource audioSource;

    float lastStepTime;
    public float minStepInterval = 0.25f;

    [Header("Footstep Clips")]
    public AudioClip[] houseSteps;
    public AudioClip[] streetSteps;
    public AudioClip[] grassSteps;

    [Header("Step Settings")]
    public float walkStepDistance = 1.6f;
    public float runStepDistance = 1.0f;
    public float cinematicStepDistance = 2.4f; // pasos más espaciados en cinemática
    public float rayDistance = 2f;

    float distanceAccumulator;
    Vector3 lastPosition;

    CharacterController controller;
    PlayerController playerController;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // =============================
        // DETECCIÓN DE SUELO PROPIA
        // =============================

        if (!IsGrounded())
        {
            lastPosition = transform.position;
            return;
        }

        // =============================
        // MEDICIÓN DE MOVIMIENTO REAL
        // =============================

        Vector3 horizontalMove = transform.position - lastPosition;
        horizontalMove.y = 0f;

        float movedDistance = Mathf.Min(horizontalMove.magnitude, 0.2f);

        if (movedDistance < 0.005f)
        {
            lastPosition = transform.position;
            return;
        }

        distanceAccumulator += movedDistance;
        lastPosition = transform.position;

        // =============================
        // DISTANCIA SEGÚN ESTADO
        // =============================

        float stepDistance = walkStepDistance;

        if (playerController != null)
        {
            if (playerController.currentState == PlayerController.PlayerState.Cinematic)
            {
                stepDistance = cinematicStepDistance;
            }
            else
            {
                bool running = Input.GetKey(KeyCode.LeftShift);
                stepDistance = running ? runStepDistance : walkStepDistance;
            }
        }

        if (distanceAccumulator >= stepDistance)
        {
            PlayFootstep();
            distanceAccumulator = 0f;
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }

    void PlayFootstep()
    {
        if (Time.time - lastStepTime < minStepInterval)
            return;

        lastStepTime = Time.time;

        AudioClip clip = GetClipFromSurface();
        if (clip == null) return;

        audioSource.pitch = Random.Range(0.88f, 1.12f);

        if (playerController != null &&
            playerController.currentState == PlayerController.PlayerState.Free &&
            Input.GetKey(KeyCode.LeftShift))
        {
            audioSource.pitch += 0.1f;
        }

        audioSource.PlayOneShot(clip);
    }

    AudioClip GetClipFromSurface()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
            return null;

        switch (hit.collider.tag)
        {
            case "Floor_House":
                return RandomClip(houseSteps);

            case "Floor_Road":
                return RandomClip(streetSteps);

            case "Floor_Grass":
                return RandomClip(grassSteps);
        }

        return null;
    }

    AudioClip RandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }
}