using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteButton : MonoBehaviour, IInteractable
{
    public enum ButtonType
    {
        Play,
        Pause,
        Stop,
        Next,
        Back
    }

    public ButtonType buttonType;
    public float skipSeconds = 5f;

    CassettePlayer player;

    void Start()
    {
        player = FindObjectOfType<CassettePlayer>();
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (player == null) return;

        switch (buttonType)
        {
            case ButtonType.Play:
                player.Play();
                break;

            case ButtonType.Pause:
                player.Pause();
                break;

            case ButtonType.Stop:
                player.Stop();
                break;

            case ButtonType.Next:
                player.Next(skipSeconds);
                break;

            case ButtonType.Back:
                player.Back(skipSeconds);
                break;
        }
    }
}