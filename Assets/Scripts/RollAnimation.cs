using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollAnimation : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    private Dice playerDice;
    private EnemyDice enemyDice;

    // Direct reference to the boolean flag in RollAnimation
    public bool isFinalRoll;

    void Start()
    {
        TryGetComponent(out playerDice);
        TryGetComponent(out enemyDice);
    }

    void Update()
    {
        if (isFinalRoll)
        {
            // Display final roll results after the animation is finished
            if (playerDice != null)
            {
                numberDisplay.text = playerDice.GetRollResult().ToString();
            }
            else if (enemyDice != null)
            {
                numberDisplay.text = enemyDice.GetRollResult().ToString();
            }
        }
        else
        {
            // During idle state, show max roll values of the player and enemy
            if (playerDice != null)
            {
                numberDisplay.text = playerDice.MaxRoll.ToString();
            }
            else if (enemyDice != null)
            {
                numberDisplay.text = enemyDice.MaxRoll.ToString();
            }
        }
    }

    // Play the roll animation for a given duration
    public IEnumerator PlayRollAnimation(float duration)
    {
        float elapsedTime = 0f;

        // Animate the roll by changing the number display over time
        while (elapsedTime < duration)
        {
            int randomRoll = Random.Range(1, 11); // Assuming dice values range from 1 to 6
            numberDisplay.text = randomRoll.ToString();
            elapsedTime += Time.deltaTime;

            // Wait for the next frame before updating again
            yield return null;
        }

        // After the animation, show the final roll result
        isFinalRoll = true;
        if (playerDice != null)
        {
            numberDisplay.text = playerDice.GetRollResult().ToString();
        }
        else if (enemyDice != null)
        {
            numberDisplay.text = enemyDice.GetRollResult().ToString();
        }
    }
}
