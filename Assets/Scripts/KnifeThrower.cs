using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    [Header("Throw Settings")]
    public float minThrowForce = 2f;
    public float maxThrowForce = 15f;
    public float minDragDistance = 0.1f;
    public float spinMultiplier = 100f;
    public float maxSpinSpeed = 100f;

    [Header("Blade Colliders for Each Knife")]
    public GameObject bladeColliderKnife1;
    public GameObject bladeColliderKnife2;
    public GameObject bladeColliderKnife3;

    private Rigidbody2D rb;
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isStopped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        bladeColliderKnife1.SetActive(false);
        bladeColliderKnife2.SetActive(false);
        bladeColliderKnife3.SetActive(false);

        int knifeIndex = CustomizationData.instance != null ? CustomizationData.instance.selectedKnifeIndex : 0;
        SetSelectedKnife(knifeIndex);  // Activates the correct blade collider
    }

    public void SetSelectedKnife(int knifeIndex)
    {
        bladeColliderKnife1.SetActive(false);
        bladeColliderKnife2.SetActive(false);
        bladeColliderKnife3.SetActive(false);

        switch (knifeIndex)
        {
            case 0:
                bladeColliderKnife1.SetActive(true);
                break;
            case 1:
                bladeColliderKnife2.SetActive(true);
                break;
            case 2:
                bladeColliderKnife3.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (!isStopped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startDragPosition.z = 0;
            }

            if (Input.GetMouseButtonUp(0))
            {
                endDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endDragPosition.z = 0;
                float dragDistance = Vector2.Distance(startDragPosition, endDragPosition);

                if (dragDistance >= minDragDistance)
                {
                    ThrowKnife(startDragPosition, endDragPosition, dragDistance);
                }
            }

            // Restrict rotation to the Z-axis only
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
        }
    }

    private void ThrowKnife(Vector3 start, Vector3 end, float dragDistance)
    {
        rb.isKinematic = false; // Make sure the knife can be affected by physics

        Vector2 throwDirection = (end - start).normalized;
        float throwForce = Mathf.Clamp(dragDistance, minThrowForce, maxThrowForce);

        // Apply forward velocity based on throw force
        rb.velocity = throwDirection * throwForce;

        // Set a moderate spin speed for the knife
        float baseSpinSpeed = 720f; // 720 degrees per second for 2 full rotations
        rb.angularVelocity = baseSpinSpeed; // Apply the initial spin

        // Adjust the knife’s trajectory
        rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse); // Apply a downward force to counteract floatiness
    }

    public void StopKnife()
    {
        if (isStopped) return; // Prevent multiple calls

        isStopped = true; // Mark knife as stopped

        Debug.Log("Stopping knife...");

        // Stop all movement and rotation
        rb.velocity = Vector2.zero;            // Stop linear movement
        rb.angularVelocity = 0;                // Stop rotation
        rb.isKinematic = true;                 // Disable further physics interactions

        // Optionally, reset immediately or after a delay
        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f); // Wait for a moment before resetting
        ResetKnife(); // Call the reset method
    }

    // Method to reset the knife's position and state
    public void ResetKnife()
    {
        // Reset the knife's position and rotation
        transform.position = initialPosition; // Reset to initial position
        transform.rotation = initialRotation; // Reset rotation

        // Reset Rigidbody2D properties
        rb.isKinematic = true; // Set Rigidbody to kinematic to stop all movement
        rb.velocity = Vector2.zero; // Ensure velocity is zero
        rb.angularVelocity = 0; // Ensure angular velocity is zero

        // Reset the isStopped flag
        isStopped = false; // Allow the knife to be thrown again

        Debug.Log("Knife has been reset.");
    }
}
