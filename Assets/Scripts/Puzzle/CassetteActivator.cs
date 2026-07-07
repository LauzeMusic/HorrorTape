using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteActivator : MonoBehaviour, IInteractable
{
    [Header("Configuración de Cambio")]
    public GameObject deskCassette; // El que aparece en el escritorio

    [Header("Feedback")]
    public InteractSFX interactSFX; // Arrastrá aquí el componente de sonido

    public void Interact(PlayerInteractor interactor)
    {
        // 1. Sonido (usando la lógica que no se corta al desactivar)
        if (interactSFX != null)
        {
            interactSFX.Play();
        }

        // 2. Activar el cassette del escritorio
        if (deskCassette != null)
        {
            deskCassette.SetActive(true);
            Debug.Log("Cassette del escritorio ACTIVADO");
        }

        // 3. DESACTIVAR este cassette (el del mapa)
        // Esto es lo que faltaba. Lo desactivamos a él mismo.
        this.gameObject.SetActive(false); 
        
        Debug.Log("Cassette del mapa DESACTIVADO");
    }
}