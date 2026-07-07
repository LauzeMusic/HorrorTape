using UnityEngine;

public class TriggerZoneDeactivator : MonoBehaviour
{
    [Header("Configuración del Trigger")]
    [Tooltip("El GameObject entero que querés desactivar (ej: la piedra, o paredes viejas)")]
    public GameObject targetGameObjectToDisable;

    [Tooltip("¿Solo el jugador puede activar esto?")]
    public bool onlyPlayer = true;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer && !other.CompareTag("Player"))
        {
            return;
        }

        // desactivar objpiedra o obstáculo
        if (targetGameObjectToDisable != null)
        {
            targetGameObjectToDisable.SetActive(false);
            Debug.Log($"[Trigger] Se desactivó el objeto: {targetGameObjectToDisable.name}");
        }

        // Desactivamor ESTE componente 
        this.gameObject.SetActive(false); 
    }
}