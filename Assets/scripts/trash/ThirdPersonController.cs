using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f; // Degrees per second
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Transform cameraTransform; // Reference to the camera
    public Vector3 cameraOffset = new Vector3(0f, 2f, -5f); // Camera position offset
    public float cameraSmoothSpeed = 10f; // Smoothing speed for camera movement

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    void LateUpdate()
    {
        HandleCameraFollow();
    }

    void HandleMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Move the character in the direction of the camera
        if (inputDirection.magnitude >= 0.1f)
        {
            // Calculate target direction based on camera
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move in the target direction
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void HandleGravity()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleCameraFollow()
    {
        if (cameraTransform != null)
        {
            // Calculate desired position
            Vector3 desiredPosition = transform.position + cameraOffset;

            // Smoothly move the camera to the desired position
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime);

            // Make the camera look at the player
            cameraTransform.LookAt(transform.position + Vector3.up * 1.5f); // Adjust the height focus point as needed
        }
    }
}
