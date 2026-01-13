using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleController : MonoBehaviour
{
    public static SubtitleController Instance;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;

    Coroutine subtitleRoutine;

    void Awake()
    {
        Instance = this;
        Clear();
    }

    public void Play(NarrativeAudioData data)
    {
        Clear();

        titleText.text = data.title;

        subtitleRoutine = StartCoroutine(RunSubtitles(data.subtitles));
    }

    IEnumerator RunSubtitles(SubtitleLine[] lines)
    {
        foreach (var line in lines)
        {
            yield return new WaitForSeconds(line.time);
            subtitleText.text = line.text;
        }
    }

    public void Stop()
    {
        if (subtitleRoutine != null)
            StopCoroutine(subtitleRoutine);

        Clear();
    }

    void Clear()
    {
        titleText.text = "";
        subtitleText.text = "";
    }
}