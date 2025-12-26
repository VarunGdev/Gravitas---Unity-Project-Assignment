using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravityStrength = 25f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private HologramPreview hologramPreview;

    public ThirdPersonCameraController cameraController;
    public Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool rotatingToGravity;
    private Quaternion targetRotation;
    private Vector3 targetGravity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        GravityController.SetGravity(Vector3.down);
        transform.rotation = Quaternion.identity;
        velocity = Vector3.zero;
        rotatingToGravity = false;
        if (cameraController != null)cameraController.SetCameraRoll(0f);
    }

    void Update()
    {
        GroundCheck();
        if (!rotatingToGravity)RotatePlayerToCamera();
        if (hologramPreview != null && hologramPreview.IsPreviewing())
        {
            velocity = Vector3.zero;
            ApplyGravity();
            UpdateAnimations(Vector2.zero);
        }
        else
        {
            Vector2 moveInput = Move();
            ApplyGravity();
            UpdateAnimations(moveInput);
        }
        HandleGravityInput();
        HandleGravityRotation();
    }

    void RotatePlayerToCamera()
    {
        if (!playerCamera) return;
        Vector3 camForward = Vector3.ProjectOnPlane(playerCamera.forward,transform.up);
        if (camForward.sqrMagnitude < 0.001f) return;
        Quaternion lookRotation = Quaternion.LookRotation(camForward, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,lookRotation,rotationSpeed * Time.deltaTime);

    }

    void HandleGravityRotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            rotatingToGravity = false;
        }
    }

    Vector2 Move()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            input.x = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
            input.y = Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0;
        }

        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            velocity = transform.up * jumpForce;
        }

        return input;
    }

    void ApplyGravity()
    {
        if (isGrounded && Vector3.Dot(velocity, transform.up) < 0)velocity = -transform.up * 2f;
        velocity += GravityController.CurrentGravity * gravityStrength * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleGravityInput()
    {
        if (Keyboard.current.eKey.wasReleasedThisFrame &&hologramPreview != null &&hologramPreview.IsPreviewing())
        {
            targetGravity = hologramPreview.GetPreviewGravity();
            GravityController.SetGravity(targetGravity);

            if (cameraController != null)
            {
                float roll = Vector3.SignedAngle(Vector3.down,GravityController.CurrentGravity,transform.forward);
                cameraController.SetCameraRoll(roll);
            }

            Vector3 newUp = -targetGravity; 
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, newUp).normalized;

            if (forward.sqrMagnitude < 0.001f) forward = Vector3.Cross(newUp, transform.right);

            targetRotation = Quaternion.LookRotation(forward, newUp);
            rotatingToGravity = true;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position,-transform.up,0.2f);
    }

    void UpdateAnimations(Vector2 moveInput)
    {
        animator.SetBool("isRunning", moveInput.magnitude > 0.1f && isGrounded);
        animator.SetBool("isFalling", !isGrounded && Vector3.Dot(velocity, transform.up) < -0.1f);
    }
}
