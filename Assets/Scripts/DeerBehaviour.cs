using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerBehaviour : MonoBehaviour
{
    public float walkSpeed = 2f;          // Speed of the deer when walking
    public float walkRange = 3f;         // Range within which the deer can walk along the X-axis
    public float idleTimeMin = 2f;       // Minimum idle time
    public float idleTimeMax = 5f;       // Maximum idle time

    private float startX;                // The starting X position of the deer
    private float targetX;               // The target X position to walk to
    private Animator animator;           // Reference to the Animator component
    private bool isWalking = false;      // Whether the deer is currently walking
    private bool isEating = false;       // Whether the deer is currently eating
    private float idleTimer;             // Timer for idle state

    void Start()
    {
        // Save the starting X position and get the Animator
        startX = transform.position.x;
        animator = GetComponent<Animator>();

        // Start in the idle state
        EnterIdleState();
    }

    void Update()
    {
        if (isWalking)
        {
            // Move the deer towards the target X position
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), walkSpeed * Time.deltaTime);

            // Stop walking when the target position is reached
            if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
            {
                EnterIdleState();
            }
        }
        else if (!isEating)
        {
            // Countdown the idle timer
            idleTimer -= Time.deltaTime;

            // When the idle timer expires, decide what to do next
            if (idleTimer <= 0)
            {
                DecideNextAction();
            }
        }
    }

    void DecideNextAction()
    {
        // Randomly choose to walk or eat
        if (Random.value < 0.5f)
        {
            StartWalking();
        }
        else
        {
            StartEating();
        }
    }

    void StartWalking()
    {
        // Set a random target position within the walk range
        targetX = Random.Range(startX - walkRange, startX + walkRange);

        // Flip the deer based on the direction of movement
        if (targetX < transform.position.x)
        {
            // Facing left
            transform.localScale = new Vector3(-1.6f, 1.6f, 0f); // Flip on X-axis
        }
        else
        {
            // Facing right
            transform.localScale = new Vector3(1.6f, 1.6f, 0f); // Default scale
        }

        // Trigger the walking animation
        animator.SetBool("IsWalking", true);

        isWalking = true;
        isEating = false; // Ensure eating is disabled
    }

    void StartEating()
    {
        // Stop walking and trigger the eating animation
        isWalking = false;
        isEating = true;
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("Eat");

        // Schedule the reverse animation after eating finishes
        float eatAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length; // Get the length of Buck_Eat
        Invoke("PlayEatReverse", eatAnimationLength);
    }

    void PlayEatReverse()
    {
        // Trigger the Buck_Eat_Reverse animation
        animator.SetTrigger("EatReverse");

        // Schedule return to idle after the reverse animation finishes
        float eatReverseLength = animator.GetCurrentAnimatorStateInfo(0).length; // Length of Buck_Eat_Reverse
        Invoke("EnterIdleState", eatReverseLength);
    }

    void EnterIdleState()
    {
        // Stop all movement and reset to idle (Look) animation
        isWalking = false;
        isEating = false;
        animator.SetBool("IsWalking", false);

        // Reset position to stop unwanted movement
        targetX = transform.position.x;

        // Set a random idle timer
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
    }
}
