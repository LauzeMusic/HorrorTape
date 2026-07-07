using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSFX : MonoBehaviour
{
    public AudioClip clipA;
    public AudioClip clipB; // opcional

    [Range(0f, 1f)]
    public float volume = 1f;

    public void Play()
    {
        AudioClip clipToPlay = clipA;

        if (clipA != null && clipB != null)
        {
            clipToPlay = Random.value < 0.5f ? clipA : clipB;
        }

        if (clipToPlay != null)
        {
            // Usamos PlayClipAtPoint para que el sonido no dependa de este GameObject
            // transform.position asegura que el sonido siga siendo 3D en el lugar correcto
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, volume);
        }
    }
}