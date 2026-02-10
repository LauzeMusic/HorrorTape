using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientVolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public string exposedParam = "AmbienceVolume";

    public float outsideVolume = 0f;
    public float insideVolume = -40f;
    public float fadeTime = 1.5f;

    Coroutine fadeRoutine;

    public void EnterHouse()
    {
        StartFade(insideVolume);
    }

    public void ExitHouse()
    {
        StartFade(outsideVolume);
    }

    void StartFade(float targetDb)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeRoutine(targetDb));
    }

    IEnumerator FadeRoutine(float targetDb)
    {
        mixer.GetFloat(exposedParam, out float currentDb);

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float db = Mathf.Lerp(currentDb, targetDb, t / fadeTime);
            mixer.SetFloat(exposedParam, db);
            yield return null;
        }

        mixer.SetFloat(exposedParam, targetDb);
    }
}