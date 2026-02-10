using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAmbientAudio : MonoBehaviour
{
    public AudioSource source;
    public float fadeTime = 1f;

    Coroutine fadeRoutine;

    public void EnterChair()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        source.loop = true;
        source.volume = 0f;
        source.Play();

        fadeRoutine = StartCoroutine(Fade(0f, 1f));
    }

    public void ExitChair()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(source.volume, 0f, true));
    }

    IEnumerator Fade(float from, float to, bool stopAtEnd = false)
    {
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(from, to, t / fadeTime);
            yield return null;
        }

        source.volume = to;

        if (stopAtEnd)
            source.Stop();
    }
}