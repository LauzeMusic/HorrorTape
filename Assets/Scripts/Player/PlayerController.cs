using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // ← ESTA ES LA CLAVee

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 150f;

    [Header("External Systems")]
    public ChairAmbientAudio chairAmbient;
    public CassetteAnimatorController cassetteAnimator;

    [Header("Run Post Process")]
    public Volume runVolume;           // Volume global con el efecto de correr
    public float runBlendSpeed = 6f;   // Velocidad de entrada/salida del efecto

    // =======================
    // PLAYER STATE
    // =======================

    public enum PlayerState
    {
        Free,       // Movimiento libre
        Cassette    // Modo silla / casetera
    }

    public PlayerState currentState = PlayerState.Free;

    // =======================
    // INTERNAL REFERENCES
    // =======================

    CharacterController controller;
    Transform cam;

    float xRot = 0f;
    Vector3 velocity;

    Transform cassetteExitPoint;

    float targetRunWeight = 0f; // Peso objetivo del postprocess al correr

    // =======================
    // INIT
    // =======================

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Seguridad: arrancar sin postprocess activo
        if (runVolume != null)
            runVolume.weight = 0f;
    }

    // =======================
    // UPDATE
    // =======================

    void Update()
    {
        HandleLook();

        if (currentState == PlayerState.Free)
            HandleMovement();

        if (currentState == PlayerState.Cassette && Input.GetKeyDown(KeyCode.Space))
            ExitCassetteMode();

        HandleRunPostProcess();
    }

    // =======================
    // MOVEMENT
    // =======================

    void HandleMovement()
    {
        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = running ? runSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // =======================
    // CAMERA LOOK
    // =======================

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;

        // Más limitado cuando estás sentado
        if (currentState == PlayerState.Cassette)
            xRot = Mathf.Clamp(xRot, -35f, 35f);
        else
            xRot = Mathf.Clamp(xRot, -70f, 70f);

        cam.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    // =======================
    // RUN POST PROCESS
    // =======================

    void HandleRunPostProcess()
    {
        if (runVolume == null) return;

        // Solo se activa si:
        // - Estás en modo libre
        // - Estás presionando Shift
        bool running =
            currentState == PlayerState.Free &&
            Input.GetKey(KeyCode.LeftShift);

        targetRunWeight = running ? 1f : 0f;

        // Suavizado para que no sea brusco
        runVolume.weight = Mathf.Lerp(
            runVolume.weight,
            targetRunWeight,
            Time.deltaTime * runBlendSpeed
        );
    }

    // =======================
    // CASSETTE MODE
    // =======================

    public void EnterCassetteMode(Transform cassettePoint, Transform exitPoint)
    {
        currentState = PlayerState.Cassette;
        cassetteExitPoint = exitPoint;

        // Sonido ambiente de la silla
        if (chairAmbient != null)
            chairAmbient.EnterChair();

        controller.enabled = false;
        transform.position = cassettePoint.position;
        transform.rotation = cassettePoint.rotation;
        controller.enabled = true;
    }

    public void ExitCassetteMode()
    {
        currentState = PlayerState.Free;

        if (chairAmbient != null)
            chairAmbient.ExitChair();

        if (cassetteAnimator != null)
            cassetteAnimator.SetFocused(false);

        if (cassetteExitPoint != null)
        {
            controller.enabled = false;
            transform.position = cassetteExitPoint.position;
            transform.rotation = cassetteExitPoint.rotation;
            controller.enabled = true;
        }
    }
}