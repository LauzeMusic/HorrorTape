using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 150f;

    public ChairAmbientAudio chairAmbient;
    public CassetteAnimatorController cassetteAnimator;

    public enum PlayerState
    {
        Free,
        Cassette
    }

    public PlayerState currentState = PlayerState.Free;

    CharacterController controller;
    Transform cam;

    float xRot = 0f;
    Vector3 velocity;

    Transform cassetteExitPoint;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();

        if (currentState == PlayerState.Free)
            HandleMovement();

        if (currentState == PlayerState.Cassette && Input.GetKeyDown(KeyCode.Space))
            ExitCassetteMode();
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;

        if (currentState == PlayerState.Cassette)
            xRot = Mathf.Clamp(xRot, -45f, 45f);
        else
            xRot = Mathf.Clamp(xRot, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    // ===== MODO CASSETTE =====

    public void EnterCassetteMode(Transform cassettePoint, Transform exitPoint)
    {
        currentState = PlayerState.Cassette;
        cassetteExitPoint = exitPoint;

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