using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public enum DoorState
    {
        Closed,
        Open
    }

    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 6f;

    public DoorState currentState = DoorState.Closed;

    Quaternion closedRotation;
    Quaternion openRotation;

    bool isMoving = false;

    // 🔊 REFERENCIA AL AUDIO
    DoorAudio doorAudio;

    void Awake()
    {
        closedRotation = transform.localRotation;

        // 🔊 buscamos el DoorAudio en el mismo objeto
        doorAudio = GetComponent<DoorAudio>();
    }

    void Update()
    {
        if (!isMoving) return;

        Quaternion target =
            currentState == DoorState.Open ? openRotation : closedRotation;

        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            target,
            openSpeed * 100f * Time.deltaTime
        );

        if (Quaternion.Angle(transform.localRotation, target) < 0.1f)
        {
            transform.localRotation = target;
            isMoving = false;
        }
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (isMoving) return;

        if (currentState == DoorState.Closed)
            Open(interactor.transform);
        else
            Close();
    }

    void Open(Transform player)
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;

        float side = Vector3.Dot(transform.right, toPlayer);
        float direction = side >= 0 ? 1f : -1f;

        openRotation =
            closedRotation *
            Quaternion.Euler(0f, openAngle * direction, 0f);

        currentState = DoorState.Open;
        isMoving = true;

        // 🔊 SONIDO DE ABRIR
        if (doorAudio != null)
            doorAudio.PlayOpen();
    }

    void Close()
    {
        currentState = DoorState.Closed;
        isMoving = true;

        // 🔊 SONIDO DE CERRAR
        if (doorAudio != null)
            doorAudio.PlayClose();
    }
}