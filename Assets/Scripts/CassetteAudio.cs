using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteAudio : MonoBehaviour

{
    public AudioSource audioSource;      // audio del cassette (la grabación)
    public AudioSource clickSource;      // sonido mecánico

    public void PlayAudio()
    {
        StartCoroutine(PlayWithClick());
    }

    private System.Collections.IEnumerator PlayWithClick()
    {
        if (clickSource != null)
        {
            clickSource.Play();
            yield return new WaitForSeconds(clickSource.clip.length * 0.8f);
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}