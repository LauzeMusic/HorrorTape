using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NarrativeAudio",
    menuName = "Audio/Narrative Audio Data"
)]
public class NarrativeAudioData : ScriptableObject
{
    public string title;
    public AudioClip clip;
    public SubtitleLine[] subtitles;
}