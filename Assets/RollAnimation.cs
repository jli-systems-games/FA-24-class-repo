using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollAnimation : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    private DiceRoller diceRoller;
    private EnemyRoller enemyRoller;

    // Direct reference to the boolean flag in RollAnimation
    public bool isFinalRoll;

    void Start()
    {
        TryGetComponent(out diceRoller);
        TryGetComponent(out enemyRoller);
    }

    void Update()
    {
        if (isFinalRoll)
        {
            // Display final roll results
            if (diceRoller != null)
            {
                numberDisplay.text = diceRoller.ResultPlayer().ToString();
            }
            else if (enemyRoller != null)
            {
                numberDisplay.text = enemyRoller.ResultEnemy().ToString();
            }
        }
        else
        {
            // Display max roll values
            if (diceRoller != null)
            {
                numberDisplay.text = diceRoller.GetMaxPlayerRoll().ToString();
            }
            else if (enemyRoller != null)
            {
                numberDisplay.text = enemyRoller.GetMaxEnemyRoll().ToString();
            }
        }
    }
}