using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // The Animator component on the character
    public Transform parentTransform; // Reference to the parent object (which will be moved)
    public float moveDistance = 3f; // Distance to move the parent object
    public float jumpDuration = 1f; // Duration of the jump movement

    public Vector3 specificTargetPosition; // The target position the character will jump to when the button is pressed

    private string[] animations = { "Breath", "Jump" }; // Array of animation names
    private string currentAnimation; // Current animation being played
    private bool isJumping = false; // Flag to indicate if jumping is in progress

    private bool isEating = false; // Flag to indicate if eating animation is playing

    void Start()
    {
        StartCoroutine(SwitchAnimation()); // Start the random animation switching routine
    }

    // Coroutine to randomly switch between animations
    IEnumerator SwitchAnimation()
    {
        while (true)
        {
            if (isEating) yield return null; // If eating, wait

            // Randomly choose an animation (either "Breath" or "Jump")
            currentAnimation = animations[Random.Range(0, animations.Length)];

            // If "Jump" is selected, perform the jump and move logic
            if (currentAnimation == "Jump")
            {
                yield return JumpAndMoveRoutine();
            }
            else
            {
                // Otherwise, just play the "Breath" animation for a random duration
                animator.Play(currentAnimation);
                float randomTime = Random.Range(5f, 10f);
                yield return new WaitForSeconds(randomTime);
            }
        }
    }

    // Jump and move the character in a random direction within a certain range
    IEnumerator JumpAndMoveRoutine()
    {
        isJumping = true;
        animator.Play("Jump");

        float elapsedTime = 0f;
        Vector3 startPosition = parentTransform.position;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        Vector3 targetPosition = startPosition + randomDirection * moveDistance;

        float maxDistance = 2f;
        targetPosition.x = Mathf.Clamp(targetPosition.x, startPosition.x - maxDistance, startPosition.x + maxDistance);
        targetPosition.z = Mathf.Clamp(targetPosition.z, startPosition.z - maxDistance, startPosition.z + maxDistance);

        // Smoothly move the parent object to the target position
        while (elapsedTime < jumpDuration)
        {
            parentTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parentTransform.position = targetPosition;
        float randomTime = Random.Range(5f, 10f);
        yield return new WaitForSeconds(randomTime);

        isJumping = false;
    }

    // Public method to be called by the UI button to trigger a jump to a predefined target
    public void JumpToPredefinedTarget()
    {
        // Stop the random animation switching and jumping routines temporarily
        StopAllCoroutines();
        
        // Start the jump to the specific target
        StartCoroutine(JumpToSpecificTarget(specificTargetPosition));
    }

    // Coroutine to handle jumping to a specific target position
    IEnumerator JumpToSpecificTarget(Vector3 target)
    {
        isJumping = true;
        animator.Play("Jump");

        float elapsedTime = 0f;
        Vector3 startPosition = parentTransform.position;

        // Smoothly move the parent object to the specific target position
        while (elapsedTime < jumpDuration)
        {
            parentTransform.position = Vector3.Lerp(startPosition, target, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parentTransform.position = target;
        isJumping = false;

        // Resume the normal random movement and animation switching logic
        StartCoroutine(SwitchAnimation());
    }

    // Public method to be called by the UI button to trigger the "Eat" animation
    public void PlayEatAnimation()
    {
        // Stop all other animations
        StopAllCoroutines();

        // Start the Eat animation
        StartCoroutine(EatRoutine());
    }

    // Coroutine to handle playing the Eat animation
    IEnumerator EatRoutine()
    {
        isEating = true; // Set the eating flag to true

        animator.Play("Eat"); // Play the "Eat" animation

        // Assuming the eat animation duration is 2 seconds, wait for the animation to complete
        yield return new WaitForSeconds(2f);

        isEating = false; // Reset the eating flag

        // Resume the normal random movement and animation switching logic
        StartCoroutine(SwitchAnimation());
    }
}

