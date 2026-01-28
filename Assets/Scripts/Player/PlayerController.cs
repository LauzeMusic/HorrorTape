using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;



public class PlayerController : MonoBehaviour
{
    [Header("Interaction Blocker")]
    public GameObject cassetteBlocker;
    
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 150f;

    [Header("External Systems")]
    public ChairAmbientAudio chairAmbient;
    public CassetteAnimatorController cassetteAnimator;

    [Header("Run Visual Effect (PostProcess OLD)")]
    public PostProcessVolume postProcessVolume;
    public float runBlendSpeed = 6f;

    public float normalFOV = 60f;
    public float runFOV = 72f;

    // =======================
    // PLAYER STATE
    // =======================

    public enum PlayerState
    {
        Free,
        Cassette
    }

    public PlayerState currentState = PlayerState.Free;

    // =======================
    // INTERNAL
    // =======================

    CharacterController controller;
    Camera cam;

    float xRot = 0f;
    Vector3 velocity;

    Transform cassetteExitPoint;

    // PostProcess refs
    Vignette vignette;
    MotionBlur motionBlur;

    float runWeight = 0f;

    // =======================
    // INIT
    // =======================

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Cachear efectos
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out vignette);
            postProcessVolume.profile.TryGetSettings(out motionBlur);
        }
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

        HandleRunEffects();
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
    // LOOK
    // =======================

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;

        if (currentState == PlayerState.Cassette)
            xRot = Mathf.Clamp(xRot, -35f, 35f);
        else
            xRot = Mathf.Clamp(xRot, -70f, 70f);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    // =======================
    // RUN EFFECTS
    // =======================

    void HandleRunEffects()
    {
        bool running =
            currentState == PlayerState.Free &&
            Input.GetKey(KeyCode.LeftShift);

        float target = running ? 1f : 0f;

        runWeight = Mathf.Lerp(runWeight, target, Time.deltaTime * runBlendSpeed);

        // FOV dinámico
        cam.fieldOfView = Mathf.Lerp(normalFOV, runFOV, runWeight);

        // Vignette (ojo de gato)
        if (vignette != null)
            vignette.intensity.value = Mathf.Lerp(0.2f, 0.45f, runWeight);

        // Motion blur leve
        if (motionBlur != null)
            motionBlur.shutterAngle.value = Mathf.Lerp(0f, 120f, runWeight);
    }

    // =======================
    // CASSETTE MODE
    // =======================

    public void EnterCassetteMode(Transform cassettePoint, Transform exitPoint)
    {
        currentState = PlayerState.Cassette;

        cassetteExitPoint = exitPoint; // ← Faltaba esta para determinar la salida

        if (cassetteBlocker != null)
            cassetteBlocker.SetActive(false);

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

        if (cassetteBlocker != null)
            cassetteBlocker.SetActive(true);

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