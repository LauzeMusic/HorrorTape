using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{    
    [Header("Audio")]
    public AudioMixer audioMixer;

    // --- MÉTODOS PARA EL MENÚ DE OPCIONES ---

    public void SetMasterVolume(float value)
    {
        if (audioMixer != null)
            audioMixer.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        if (audioMixer != null)
            audioMixer.SetFloat("Musica", value);
    }

    public void SetSFXVolume(float value)
    {
        if (audioMixer != null)
            audioMixer.SetFloat("Sfx", value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Resolution currentRes = Screen.currentResolution;
            Screen.SetResolution(currentRes.width, currentRes.height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Resolution currentRes = Screen.currentResolution;
            Screen.SetResolution(currentRes.width, currentRes.height, FullScreenMode.Windowed);
        }

        Screen.fullScreen = isFullscreen;
        Debug.Log("In-Game Fullscreen: " + isFullscreen);
    }
}