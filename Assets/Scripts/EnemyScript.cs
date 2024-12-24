using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StringEvent : UnityEvent<string> { }

public class EnemyScript : MonoBehaviour
{
    public UnityEvent<string> onAttackPrompt;

    [Header("Indicators")]
    public GameObject UpIndicator;
    public GameObject DownIndicator;
    public GameObject LeftIndicator;
    public GameObject RightIndicator;

    [Header("Audio Clips")]
    public AudioClip upSound;
    public AudioClip downSound;
    public AudioClip leftSound;
    public AudioClip rightSound;

    [Header("Timing Parameters")]
    public float baseIndicatorDuration = 2f;
    public float minIndicatorDuration = 0.75f;
    public float minPromptInterval = 0.5f;

    [Header("Blinking Parameters")]
    public int blinkCount = 3;
    public float blinkInterval = 0.2f;

    [Header("Animations")]
    public AnimationClip StageOne;
    public Animator animator; 

    private string[] directions = { "Up", "Down", "Left", "Right" };
    private string currentDirection;

    private AudioSource audioSource;
    private float indicatorDuration;
    private float promptInterval = 2f;
    private bool isAttacking = false;

    private GameManager gameManager; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onDifficultyIncrease.AddListener(UpdateDifficulty);

        indicatorDuration = baseIndicatorDuration;

        animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.SetInteger("DifficultyStage", gameManager.difficultyStage);

    }


    public void StartAttacks()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackCycle());
        }
    }

    private IEnumerator AttackCycle()
    {
        while (isAttacking)
        {
            yield return new WaitForSeconds(promptInterval);
            PromptAttack();
        }
    }


    private void PromptAttack()
    {
        currentDirection = directions[Random.Range(0, directions.Length)];
        onAttackPrompt?.Invoke(currentDirection);
        ShowDirectionVisual(currentDirection);
        PlaySound(currentDirection);
        //TriggerAnimation(currentDirection);
    }

    //private void TriggerAnimation(string direction)
    //{
    //    if (animator == null) return;

    //    ResetAttackTriggers();

    //    switch (direction)
    //    {
    //        case "Up":
    //            animator.SetTrigger("Enemy_Up");
    //            break;
    //        case "Down":
    //            animator.SetTrigger("Enemy_Down");
    //            break;
    //        case "Left":
    //            animator.SetTrigger("Enemy_Left");
    //            break;
    //        case "Right":
    //            animator.SetTrigger("Enemy_Right");
    //            break;
    //        default:
    //            Debug.LogWarning($"Invalid direction: {direction}");
    //            break;
    //    }
    //}

    //private void ResetAttackTriggers()
    //{
    //    animator.ResetTrigger("Enemy_Up");
    //    animator.ResetTrigger("Enemy_Down");
    //    animator.ResetTrigger("Enemy_Left");
    //    animator.ResetTrigger("Enemy_Right");
    //}

    private void ShowDirectionVisual(string direction)
    {
        GameObject indicator = direction switch
        {
            "Up" => UpIndicator,
            "Down" => DownIndicator,
            "Left" => LeftIndicator,
            "Right" => RightIndicator,
            _ => null
        };

        if (indicator != null)
        {
            StartCoroutine(BlinkIndicator(indicator));
        }
    }

    private IEnumerator BlinkIndicator(GameObject indicator)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            indicator.SetActive(!indicator.activeSelf);
            yield return new WaitForSeconds(blinkInterval);
        }

        indicator.SetActive(false); 
        float remainingTime = Mathf.Max(indicatorDuration - (blinkCount * blinkInterval), 0.1f);
        yield return new WaitForSeconds(remainingTime);
    }

    private void PlaySound(string direction)
    {
        AudioClip clip = direction switch
        {
            "Up" => upSound,
            "Down" => downSound,
            "Left" => leftSound,
            "Right" => rightSound,
            _ => null
        };

        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void UpdateDifficulty(int difficultyStage)
    {
        promptInterval = Mathf.Max(2f / difficultyStage, minPromptInterval);
        indicatorDuration = Mathf.Max(baseIndicatorDuration / difficultyStage, minIndicatorDuration);
        animator.SetInteger("DifficultyStage", difficultyStage);
    }
}

