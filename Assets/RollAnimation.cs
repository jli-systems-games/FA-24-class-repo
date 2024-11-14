using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollAnimation : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    public int finalValue;

    private DiceRoller diceRoller;
    private EnemyRoller enemyRoller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TryGetComponent(out diceRoller))
        {
            DisplayMaxRoll(diceRoller.GetMaxPlayerRoll());
        }
        else if (TryGetComponent(out enemyRoller))
        {
            DisplayMaxRoll(enemyRoller.GetMaxEnemyRoll());
        }
    }

    private void DisplayMaxRoll(int maxRoll)
    {
        numberDisplay.text = maxRoll.ToString();
    }

    public void StartRollAnimation(int value, bool isPlayer)
    {
        finalValue = value;

        if (isPlayer)
        {
            numberDisplay.text = "Rolling Player: ";
        }
        else
        {
            numberDisplay.text = "Rolling Enemy: ";
        }

        StartCoroutine(AnimateRoll(isPlayer));
    }

    private IEnumerator AnimateRoll(bool isPlayer)
    {
        float elapsedTime = 0f;

        // While animation is running, display random values
        while (elapsedTime < 1.0f)  // For 1 second duration
        {
            int randomValue = Random.Range(1, finalValue + 1);

            if (isPlayer)
            {
                // Update the text for the player during animation
                numberDisplay.text = "Rolling Player: " + randomValue;
            }
            else
            {
                // Update the text for the enemy during animation
                numberDisplay.text = "Rolling Enemy: " + randomValue;
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(0.05f); // Animate every 0.05 seconds
        }

        // After animation ends, show the final value
        if (isPlayer)
        {
            // Show the final value for player
            numberDisplay.text = "Player Roll: " + finalValue;
        }
        else
        {
            // Show the final value for enemy
            numberDisplay.text = "Enemy Roll: " + finalValue;
        }
    }
}
