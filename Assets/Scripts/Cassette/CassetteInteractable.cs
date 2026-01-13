using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteInteractable : MonoBehaviour, IInteractable
{
    public NarrativeAudioData cassetteData;

    public void Interact(PlayerInteractor interactor)
    {
        CassettePlayer player = FindObjectOfType<CassettePlayer>();
        player.SetCassette(cassetteData);
    }
}