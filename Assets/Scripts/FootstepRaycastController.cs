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
    public float rayDistance = 2f;

    float distanceAccumulator;
    Vector3 lastPosition;

    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!controller.isGrounded) return;

        Vector3 horizontalMove = transform.position - lastPosition;
        horizontalMove.y = 0f;

        float movedDistance = Mathf.Min(horizontalMove.magnitude, 0.15f);
        if (movedDistance < 0.01f) return;

        distanceAccumulator += movedDistance;
        lastPosition = transform.position;

        bool running = Input.GetKey(KeyCode.LeftShift);
        float stepDistance = running ? runStepDistance : walkStepDistance;

        if (distanceAccumulator >= stepDistance)
        {
            PlayFootstep();
            distanceAccumulator = 0f;
        }
    }

    void PlayFootstep()
    {
        if (Time.time - lastStepTime < minStepInterval)
            return;

        lastStepTime = Time.time;

        AudioClip clip = GetClipFromSurface();
        if (clip == null) return;

        audioSource.pitch = Random.Range(0.88f, 1.12f);
        if (Input.GetKey(KeyCode.LeftShift))
            audioSource.pitch += 0.1f;

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

            case "Floor_Road":   // ← nuevo nombre
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