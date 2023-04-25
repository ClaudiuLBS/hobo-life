using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    CameraManager cameraManager;
    Transform cameraObject;
    HoboInput playerInput;
    Animator animator;
    Rigidbody rb;
    Vector2 movementInput;
    Vector2 cameraInput;
    Vector3 moveDirection;

    [Header("Camera Inputs")]
    public float cameraInputX;
    public float cameraInputY;

    [Header("Movement Speeds")]
    public float walkingSpeed = 3.5f;
    public float runningSpeed = 10f;
    public float rotationSpeed = 5f;

    [Header("Jumping and Falling")]
    public LayerMask groundLayer;
    public float raycastHeightOffset = 0.5f;
    public float jumpPower = 10f;
    public float gravity;

    float moveAmount;
    float verticalInput;
    float horizontalInput;
    float inAirTimer;

    bool sprintInput;
    bool isGrounded;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cameraManager = FindObjectOfType<CameraManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }


    private void Update()
    {
        HandleMovementInput();
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleFallingAndLanding();
    }
    private void LateUpdate()
    {
        cameraManager.HandleCameraMovement();   
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new();
            playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInput.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerInput.PlayerActions.Sprint.performed += _ => sprintInput = true;
            playerInput.PlayerActions.Sprint.canceled += _ => sprintInput = false;
            playerInput.PlayerActions.Jump.performed += _ => HandleJumping();

        }
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        if (sprintInput && moveAmount > 0)
            moveAmount += 0.5f;
        animator.SetFloat("speed", moveAmount);

    }

    private void HandleMovement()
    {
        Vector3 cameraForword = cameraObject.forward;
        cameraForword.y = 0;
        Vector3 cameraRight = cameraObject.right;
        cameraRight.y = 0;

        moveDirection = cameraForword * verticalInput + cameraRight * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        moveDirection *= sprintInput ? runningSpeed : walkingSpeed;

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
    }
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * verticalInput;
        targetDirection += cameraObject.right * horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    private void HandleJumping()
    {
        if (isGrounded)
        {
            animator.SetTrigger("jump");
            float jumpingVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * jumpPower);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            rb.velocity = playerVelocity;
        }
    }


    private void HandleFallingAndLanding()
    {
        if (!isGrounded)
        {
            rb.AddForce(-Vector3.up * gravity * gravity * inAirTimer);
            inAirTimer += Time.deltaTime;
        } else
            inAirTimer = 0;

        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += raycastHeightOffset;
        animator.SetBool("isGrounded", isGrounded);
        isGrounded = Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out _, groundLayer);
    }
}
