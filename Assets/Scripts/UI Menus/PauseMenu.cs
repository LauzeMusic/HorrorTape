using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseMenuUI;

    [Header("Audio")]
    public AudioMixer audioMixer;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // =======================
    // PAUSA / RESUME
    // =======================

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // =======================
    // AUDIO (dB directos)
    // =======================

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Musica", value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("Sfx", value);
    }

    // =======================
    // VIDEO (Actualizado)
    // =======================

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

    // =======================
    // NAVEGACIÓN
    // =======================

    public void BackToMainMenu()
    {
        // IMPORTANTE: Aseguramos que el tiempo vuelva a la normalidad 
        // y el cursor sea visible antes de cambiar de escena
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}