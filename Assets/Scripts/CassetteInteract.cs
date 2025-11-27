using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteInteract : MonoBehaviour
{
    public Camera playerCamera;
    public Camera cassetteCamera;
    public PlayerController playerMovement;

    public float clickDistance = 3f;

    bool playerInRange = false;
    bool inCassetteMode = false;
    CassetteHover lastHover = null;
    
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleCassetteMode();
        }

        if (inCassetteMode)
        {
            HandleCassetteClicks();
        }
    }

    void ToggleCassetteMode()
    {
        inCassetteMode = !inCassetteMode;

        playerCamera.gameObject.SetActive(!inCassetteMode);
        cassetteCamera.gameObject.SetActive(inCassetteMode);

        playerMovement.enabled = !inCassetteMode;

        Cursor.visible = inCassetteMode;
        Cursor.lockState = inCassetteMode ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void HandleCassetteClicks()
    {
        Ray ray = cassetteCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, clickDistance))
        {
        // --- HOVER ---
        CassetteHover hover = hit.collider.GetComponent<CassetteHover>();
        if (hover != null)
        {
            if (lastHover != hover)
            {
                if (lastHover != null)
                    lastHover.Unhighlight();

                hover.Highlight();
                    lastHover = hover;
                }
            }
            else if (lastHover != null)
            {
                lastHover.Unhighlight();
                lastHover = null;
            }

            // --- CLICK IZQUIERDO ---
            if (Input.GetMouseButtonDown(0))
            {
                CassetteAudio c = hit.collider.GetComponent<CassetteAudio>();
                if (c != null)
                {
                    c.PlayAudio();
                }
            }
        }
        else if (lastHover != null)
        {
            lastHover.Unhighlight();
            lastHover = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}