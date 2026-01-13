using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    public AudioSource audioSource;

    NarrativeAudioData currentAudio;
    bool isPaused = false;

    void Awake()
    {
        audioSource.playOnAwake = false;
    }

    // Se llama cuando seleccionás un cassette
    public void SetCassette(NarrativeAudioData data)
    {
        currentAudio = data;
        audioSource.clip = data.clip;
        audioSource.Stop();
        isPaused = false;

        Debug.Log("Cassette cargado: " + data.title);
    }

    public void Play()
    {
        if (currentAudio == null) return;

        audioSource.Play();
        isPaused = false;

        SubtitleController.Instance.Play(currentAudio);
    }

    public void Pause()
    {
        if (!audioSource.isPlaying) return;

        audioSource.Pause();
        isPaused = true;
    }

    public void Stop()
    {
        audioSource.Stop();
        isPaused = false;

        SubtitleController.Instance.Stop();
    }

    public void Next(float seconds = 5f)
    {
        if (audioSource.clip == null) return;

        audioSource.time = Mathf.Min(
            audioSource.time + seconds,
            audioSource.clip.length
        );
    }

    public void Back(float seconds = 5f)
    {
        if (audioSource.clip == null) return;

        audioSource.time = Mathf.Max(
            audioSource.time - seconds,
            0f
        );
    }
}