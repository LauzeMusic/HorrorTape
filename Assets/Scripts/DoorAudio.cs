using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip openClip;
    public AudioClip closeClip;

    [Header("Randomization")]
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
    public Vector2 volumeRange = new Vector2(0.9f, 1.0f);

    public void PlayOpen()
    {
        if (openClip == null) return;

        ApplyRandom();
        audioSource.PlayOneShot(openClip);
    }

    public void PlayClose()
    {
        if (closeClip == null) return;

        ApplyRandom();
        audioSource.PlayOneShot(closeClip);
    }

    void ApplyRandom()
    {
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
    }
}