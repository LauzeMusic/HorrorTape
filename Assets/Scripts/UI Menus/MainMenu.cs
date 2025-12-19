using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public ScreenFade fade;

    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return fade.FadeOut();
        SceneManager.LoadScene("Testing");
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