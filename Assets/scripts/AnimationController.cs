using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public Transform parentTransform;
    public GameObject wastePrefab;
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
    private int currentHunger = 5;

    public Slider moodSlider;
    private int maxMood = 20;
    private int currentMood = 6;

    public Slider fashionSlider;  
    private int maxFashion = 10; 
    private int currentFashion = 3; 

    public GameObject happyFace;   
    public GameObject sadFace;     

    // References to the fill images of sliders
    public Image hungerFillImage;
    public Image moodFillImage;
    public Image fashionFillImage;

    // Reference to the foot GameObject
    public GameObject footObject;  // Add this line

    void Start()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;

        moodSlider.maxValue = maxMood;
        moodSlider.value = currentMood;

        fashionSlider.maxValue = maxFashion; 
        fashionSlider.value = currentFashion; 

        StartCoroutine(SwitchAnimation());
    }

    void Update()
    {
        UpdateFaceExpressions();
        UpdateSliderColors(); // Update slider colors based on their values
        UpdateFootVisibility(); // Update foot visibility based on fashion level
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

    private void UpdateSliderColors()
    {
        // Change hunger slider fill color
        if (currentHunger < 3)
        {
            hungerFillImage.color = Color.red;
        }
        else
        {
            hungerFillImage.color = Color.white; // Or your default color
        }

        // Change mood slider fill color
        if (currentMood < 4)
        {
            moodFillImage.color = Color.red;
        }
        else
        {
            moodFillImage.color = Color.white; // Or your default color
        }

        // Change fashion slider fill color
        if (currentFashion < 3)
        {
            fashionFillImage.color = Color.red;
        }
        else
        {
            fashionFillImage.color = Color.white; // Or your default color
        }
    }

    private void UpdateFootVisibility() // Add this method
    {
        // Set foot visibility based on fashion level
        footObject.SetActive(currentFashion > 5);
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

            if (wasteSpawnPoint != null)
            {
                Instantiate(wastePrefab, wasteSpawnPoint.position, Quaternion.identity);
                DecreaseMoodByP();
            }
        }
    }

    public void DecreaseHunger()
    {
        if (currentHunger > 0)
        {
            currentHunger--;
            hungerSlider.value = currentHunger;
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
            currentMood -= 5;
            moodSlider.value = currentMood;
        }
    }

    public void IncreaseMood()
    {
        if (currentMood < maxMood)
        {
            currentMood++;
            moodSlider.value = currentMood;
        }
    }
}