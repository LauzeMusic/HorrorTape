using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip blockedClip;

    [Header("Randomization")]
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);

    public void PlayOpen()
    {
        if (openClip == null) return;

        ApplyRandomPitch();
        audioSource.PlayOneShot(openClip);
    }

    public void PlayClose()
    {
        if (closeClip == null) return;

        ApplyRandomPitch();
        audioSource.PlayOneShot(closeClip);
    }

    public void PlayBlocked()
    {
        if (blockedClip == null) return;

        ApplyRandomPitch();
        audioSource.PlayOneShot(blockedClip);
    }

    void ApplyRandomPitch()
    {
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
    }
}