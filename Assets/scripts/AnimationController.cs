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
    private int currentHunger = 5;  // Starting value for hunger

    public Slider moodSlider;
    private int maxMood = 20;
    private int currentMood = 6;

    
    public GameObject happyFace;   
    public GameObject sadFace;     

    void Start()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;

        moodSlider.maxValue = maxMood;
        moodSlider.value = currentMood;

       
        StartCoroutine(AutoDecreaseHunger());

        
        StartCoroutine(SwitchAnimation());
    }

    void Update()
    {
        UpdateFaceExpressions();
    }

    
    private void UpdateFaceExpressions()
    {
        if (currentMood > 15)
        {
            happyFace.SetActive(true);
            sadFace.SetActive(false);
        }
        else if (currentMood < 5)
        {
            happyFace.SetActive(false);
            sadFace.SetActive(true);
        }
        else
        {
            happyFace.SetActive(false);
            sadFace.SetActive(false);
        }
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

    // Function to handle increasing hunger
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

    // Function to handle decreasing hunger over time
    private void DecreaseHunger()
    {
        if (currentHunger > 0)
        {
            currentHunger--;
            hungerSlider.value = currentHunger;
        }
    }

    // Coroutine for automatically decreasing hunger every 2 seconds
    private IEnumerator AutoDecreaseHunger()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);  // Wait 2 seconds between each hunger decrease
            DecreaseHunger();
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
