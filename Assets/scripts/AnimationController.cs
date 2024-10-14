using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // The Animator component on the character
    public Transform parentTransform; // Reference to the parent object (which will be moved)
    public float moveDistance = 3f; // Distance to move the parent object
    public float jumpDuration = 1f; // Duration of the jump movement

    private string[] animations = { "Breath", "Jump" }; // Array of animation names
    private string currentAnimation; // Current animation being played
    private bool isJumping = false; // Flag to indicate if jumping is in progress

    void Start()
    {
        StartCoroutine(SwitchAnimation());
    }

    IEnumerator SwitchAnimation()
    {
        while (true)
        {
            // Randomly select the next animation
            currentAnimation = animations[Random.Range(0, animations.Length)];

            // If the current animation is "Jump", start the jump movement
            if (currentAnimation == "Jump")
            {
                yield return JumpAndMoveRoutine();
            }
            else
            {
                // Play the "Breath" animation (or any non-jump animation)
                animator.Play(currentAnimation);

                // Wait for a random duration between 5 and 10 seconds
                float randomTime = Random.Range(5f, 10f);
                yield return new WaitForSeconds(randomTime);
            }
        }
    }

    IEnumerator JumpAndMoveRoutine()
{
    isJumping = true;

    // Play the "Jump" animation
    animator.Play("Jump");

    // Get the starting position of the parent object (preserving the current y position)
    float elapsedTime = 0f;
    Vector3 startPosition = parentTransform.position;

    // Generate a random direction vector and normalize it
    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

    // Define the target position by moving in the random direction, limited by moveDistance
    Vector3 targetPosition = startPosition + randomDirection * moveDistance;

    // Limit the movement within a specific range (for example, using clamp)
    float maxDistance = 5f; // Set maximum distance from the original position
    targetPosition.x = Mathf.Clamp(targetPosition.x, startPosition.x - maxDistance, startPosition.x + maxDistance);
    targetPosition.z = Mathf.Clamp(targetPosition.z, startPosition.z - maxDistance, startPosition.z + maxDistance);

    while (elapsedTime < jumpDuration)
    {
        // Interpolate the position only along x and z axes
        parentTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / jumpDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the parent reaches the final target position
    parentTransform.position = targetPosition;

    // Wait for a random duration between 5 and 10 seconds after jumping
    float randomTime = Random.Range(5f, 10f);
    yield return new WaitForSeconds(randomTime);

    isJumping = false;
}


}
