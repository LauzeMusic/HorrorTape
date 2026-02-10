using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public Door doorToUnlock;

    public void Interact(PlayerInteractor interactor)
    {
        doorToUnlock.UnlockDoor();
        gameObject.SetActive(false); // desaparece la llave
    }
}