using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractSFX : MonoBehaviour
{
    public AudioClip clipA;
    public AudioClip clipB; // opcional

    [Range(0f, 1f)]
    public float volume = 1f;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f; // 3D
    }

    public void Play()
    {
        AudioClip clipToPlay = clipA;

        if (clipA != null && clipB != null)
        {
            clipToPlay = Random.value < 0.5f ? clipA : clipB;
        }

        if (clipToPlay != null)
            source.PlayOneShot(clipToPlay, volume);
    }
}