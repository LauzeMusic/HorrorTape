using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactDistance = 2f;
    public LayerMask interactableLayer;
    public Camera playerCamera;

    IInteractable currentInteractable;
    HoverHighlight currentHover;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out hit,
            interactDistance,
            interactableLayer))
        {
            currentInteractable = hit.collider.GetComponentInParent<IInteractable>();
            HoverHighlight hover = hit.collider.GetComponentInParent<HoverHighlight>();

            if (hover != currentHover)
            {
                ClearHover();
                currentHover = hover;
                currentHover?.OnHoverEnter();
            }

            if (Input.GetMouseButtonDown(0) && currentInteractable != null)
            {
                // 🔊 Sonido genérico de interacción
                hit.collider
                    .GetComponent<InteractSFX>()?
                    .Play();

                // 👉 Lógica propia del objeto
                currentInteractable.Interact(this);
            }
        }
        else
        {
            ClearHover();
            currentInteractable = null;
        }
    }

    void ClearHover()
    {
        if (currentHover != null)
        {
            currentHover.OnHoverExit();
            currentHover = null;
        }
    }
}