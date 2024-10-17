using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public Transform parentTransform;
    public GameObject wastePrefab;  // Waste prefab
    public Transform wasteSpawnPoint;  // Reference to the empty GameObject's transform
    public float moveDistance = 3f;
    public float jumpDuration = 1f;

    public Vector3 specificTargetPosition;
    public Vector3 roomCenter;
    public Vector3 roomSize;

    private string[] animations = { "Breath", "Jump" };
    private string currentAnimation;
    private bool isJumping = false;
    private bool isEating = false;

    public Slider hungerSlider;
    private int maxHunger = 10;
    private int currentHunger = 0;

    public Slider moodSlider;
    private int maxMood = 20;
    private int currentMood = 5;

    void Start()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;

        moodSlider.maxValue = maxMood;
        moodSlider.value = currentMood;

        StartCoroutine(SwitchAnimation());
    }

    IEnumerator SwitchAnimation()
    {
        while (true)
        {
            if (isEating) yield return null;

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

    IEnumerator JumpAndMoveRoutine()
    {
        isJumping = true;
        animator.Play("Jump");

        float elapsedTime = 0f;
        Vector3 startPosition = parentTransform.position;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        Vector3 targetPosition = startPosition + randomDirection * moveDistance;

        targetPosition.x = Mathf.Clamp(targetPosition.x, roomCenter.x - roomSize.x / 2, roomCenter.x + roomSize.x / 2);
        targetPosition.z = Mathf.Clamp(targetPosition.z, roomCenter.z - roomSize.z / 2, roomCenter.z + roomSize.z / 2);

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

    public void JumpToPredefinedTarget()
    {
        DecreaseMood();
        StopAllCoroutines();
        StartCoroutine(JumpToSpecificTarget(specificTargetPosition));
    }

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

        animator.Play("Breath");
        yield return new WaitForSeconds(3f);

        StartCoroutine(SwitchAnimation());
    }

    public void PlayEatAnimation()
    {
        IncreaseHunger();
        IncreaseMood();
        StopAllCoroutines();
        StartCoroutine(EatRoutine());
    }

    IEnumerator EatRoutine()
    {
        isEating = true;
        animator.Play("Eat");
        yield return new WaitForSeconds(1f);
        isEating = false;
        StartCoroutine(SwitchAnimation());
    }

    private void IncreaseHunger()
    {
        if (currentHunger < maxHunger)
        {
            currentHunger++;
            hungerSlider.value = currentHunger;
        }

        if (currentHunger >= maxHunger)
        {
            currentHunger = 0;
            hungerSlider.value = currentHunger;

            // Instantiate wastePrefab at the empty GameObject's position (wasteSpawnPoint)
            if (wasteSpawnPoint != null)
            {
                Instantiate(wastePrefab, wasteSpawnPoint.position, Quaternion.identity);
                DecreaseMoodByP();
            }
        }
    }

    public void DecreaseMood()
    {
        if (currentMood > 0)
        {
            currentMood--;
            moodSlider.value = currentMood;
        }
    }

    public void DecreaseMoodByP()
    {
        if (currentMood > 0)
        {
            currentMood -= 10;
            moodSlider.value = currentMood;
        }
    }

    public void IncreaseMood()
    {
        if (currentMood < maxMood)
        {
            currentMood += 1;
            moodSlider.value = currentMood;
        }
    }
}