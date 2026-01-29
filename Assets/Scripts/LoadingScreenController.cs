using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float displayTime = 10f;
    public float fadeTime = 2f;

    public PlayerController player;

    void Start()
    {
        player.EnterLoading();
        StartCoroutine(LoadingRoutine());
    }

    IEnumerator LoadingRoutine()
    {
        yield return new WaitForSeconds(displayTime);

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);

        player.ExitLoading(); // pasa a CINEMATIC
    }
}