using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StringEvent : UnityEvent<string> { }

public class EnemyScript : MonoBehaviour
{
    public UnityEvent<string> onAttackPrompt;

    private string[] directions = { "Up", "Down", "Left", "Right" };
    private string currentDirection;

    public GameObject UpIndicator;
    public GameObject DownIndicator;
    public GameObject LeftIndicator;
    public GameObject RightIndicator;

    public float indicatorDuration = 1f;
    private float promptInterval = 2f;
    private bool isAttacking = false;

    private float minPromptInterval = 0.5f;

    private void Start()
    {
        FindObjectOfType<GameManager>().onDifficultyIncrease.AddListener(UpdatePromptInterval);
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
    }

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
            StartCoroutine(ActivateIndicator(indicator));
        }
    }

    private IEnumerator ActivateIndicator(GameObject indicator)
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(indicatorDuration);
        indicator.SetActive(false);
    }

    private void UpdatePromptInterval(int difficultyStage)
    {
        promptInterval = Mathf.Max(2f / difficultyStage, minPromptInterval);
    }
}
