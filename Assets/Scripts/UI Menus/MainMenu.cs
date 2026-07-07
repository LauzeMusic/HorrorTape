using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public ScreenFade fade;

    [Header("Sonidos Aleatorios")]
    public AudioSource menuSource; // AudioSource 
    public List<AudioClip> menuSounds; // Lista para agregar/quitar sonidos en el Inspector

    [Header("Links")]
    public string itchIoUrl = "https://lauzemusic.itch.io/"; 

    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return fade.FadeOut();
        SceneManager.LoadScene("Testing");
    }

    // Sonido Aleatorio
    public void PlayRandomSound()
    {
        if (menuSounds.Count > 0 && menuSource != null)
        {
            // Elegimos un índice al azar de la lista
            int randomIndex = Random.Range(0, menuSounds.Count);
            
            // Reproducimos sin cortar lo que esté sonando (por si clickean rápido)
            menuSource.PlayOneShot(menuSounds[randomIndex]);
        }
        else
        {
            Debug.LogWarning("Faltan sonidos en la lista o no asignaste el AudioSource.");
        }
    }

    // Función para el segundo botón: Link a Itch.io
    public void OpenItchIo()
    {
        Application.OpenURL(itchIoUrl);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}