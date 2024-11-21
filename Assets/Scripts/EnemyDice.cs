using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDice : Dice
{
    private const int DefaultMaxEnemyLevel = 5;
    private int enemyLevel;

    public void InitializeEnemy(int playerMaxRoll)
    {
        int minRoll = Mathf.Max(1, playerMaxRoll / 2);
        int maxRoll = Mathf.Min(playerMaxRoll + 5, playerMaxRoll * 2);
        SetRollRange(minRoll, maxRoll);

        Debug.Log($"Enemy Dice Initialized with Range: {MinRoll}-{MaxRoll}");
    }

    private void SetEnemyRange()
    {
        enemyLevel = Random.Range(1, DefaultMaxEnemyLevel + 1);
        MaxRoll = 6 + (enemyLevel * enemyLevel) * 2;
        Debug.Log($"Enemy initialized with level {enemyLevel} and max roll {MaxRoll}");
    }

    public int GetMaxRoll()
    {
        return MaxRoll;
    }
}
