using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteActivator : MonoBehaviour, IInteractable
{
    public GameObject deskCassette;

    public void Interact(PlayerInteractor interactor)
    {
        if (deskCassette != null)
            deskCassette.SetActive(true);
    }
}