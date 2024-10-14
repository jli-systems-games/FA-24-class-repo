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

    void Start()
    {
        StartCoroutine(SwitchAnimation());
    }

    // Coroutine to randomly switch between animations
    IEnumerator SwitchAnimation()
    {
        while (true)
        {
            currentAnimation = animations[Random.Range(0, animations.Length)];

            if (currentAnimation == "Jump")
            {
                yield return JumpAndMoveRoutine();
            }
            else
            {
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
        if (!isJumping)
        {
            StartCoroutine(JumpToSpecificTarget(specificTargetPosition));
        }
    }

    // Coroutine to handle jumping to a specific target position
    IEnumerator JumpToSpecificTarget(Vector3 target)
    {
        isJumping = true;
        animator.Play("Jump");

        float elapsedTime = 0f;
        Vector3 startPosition = parentTransform.position;

        while (elapsedTime < jumpDuration)
        {
            parentTransform.position = Vector3.Lerp(startPosition, target, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parentTransform.position = target;
        isJumping = false;
    }
}
