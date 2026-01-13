using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    void Interact(PlayerInteractor interactor);
}
// Cualquier objeto que quiera ser interactuable debe tener una función Interact