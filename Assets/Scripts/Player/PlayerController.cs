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

    [Header("Run Visual Effect")]
    public PostProcessVolume postProcessVolume;
    public float runBlendSpeed = 6f;
    public float normalFOV = 60f;
    public float runFOV = 72f;

    // =======================
    // PLAYER STATE
    // =======================

    public enum PlayerState
    {
        Loading,
        Cinematic,
        Free,
        Cassette
    }

    public PlayerState currentState = PlayerState.Loading;

    // =======================
    // INTERNAL
    // =======================

    CharacterController controller;
    Camera cam;

    float xRot = 0f;
    Vector3 velocity;

    Transform cassetteExitPoint;

    Vignette vignette;
    MotionBlur motionBlur;

    float runWeight = 0f;

    // =======================
    // INIT
    // =======================

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        if (currentState == PlayerState.Loading)
            return;

        HandleLook();

        if (currentState == PlayerState.Free)
            HandleMovement();

        if (currentState == PlayerState.Cassette && Input.GetKeyDown(KeyCode.Space))
            ExitCassetteMode();

        HandleRunEffects();
    }

    // =====================
    // STATE CONTROL
    // =====================

    public void EnterLoading()
    {
        currentState = PlayerState.Loading;

        if (controller != null)
            controller.enabled = false;
    }

    public void ExitLoading()
    {
        currentState = PlayerState.Cinematic;
    }

    public void EnterCinematic()
    {
        currentState = PlayerState.Cinematic;
        controller.enabled = false;
        velocity = Vector3.zero;
    }

    public void ExitCinematic()
    {
        controller.enabled = true;
        currentState = PlayerState.Free;
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

        cam.fieldOfView = Mathf.Lerp(normalFOV, runFOV, runWeight);

        if (vignette != null)
            vignette.intensity.value = Mathf.Lerp(0.2f, 0.45f, runWeight);

        if (motionBlur != null)
            motionBlur.shutterAngle.value = Mathf.Lerp(0f, 120f, runWeight);
    }

    // =======================
    // CASSETTE MODE
    // =======================

    public void EnterCassetteMode(Transform cassettePoint, Transform exitPoint)
    {
        currentState = PlayerState.Cassette;
        cassetteExitPoint = exitPoint;

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