using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rb;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;

    private int jumpCount = 0;
    private int maxJumps = 2;

    public int JumpCount => jumpCount;

    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump if the Jump button is pressed and jump count allows it.
        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumps))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity for consistent jump height
            rb.AddForce(Vector3.up * 100 * jumpStrength);
            jumpCount++;
            Jumped?.Invoke(); // Trigger jump event
        }

        // Reset jump count if grounded.
        if (groundCheck && groundCheck.isGrounded)
        {
            jumpCount = 0;
        }
    }
}
