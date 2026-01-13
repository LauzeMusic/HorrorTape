using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour

{

    public AudioMixer audioMixer;

    public void SetMaster(float db)
    {
        audioMixer.SetFloat("MasterVolume", db);
    }

    public void SetMusic(float db)
    {
        audioMixer.SetFloat("Musica", db);
    }

    public void SetSFX(float db)
    {
        audioMixer.SetFloat("Sfx", db);
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}