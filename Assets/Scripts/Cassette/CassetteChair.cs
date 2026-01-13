using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteChair : MonoBehaviour, IInteractable
{
    public Transform cassettePoint;
    public Transform exitPoint;
    public CassetteAnimatorController cassetteAnimator;

    public void Interact(PlayerInteractor interactor)
    {
        PlayerController player = interactor.GetComponent<PlayerController>();
        if (player == null) return;

        player.EnterCassetteMode(cassettePoint, exitPoint);
        cassetteAnimator.SetFocused(true);
    }
}