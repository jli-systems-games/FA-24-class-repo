using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDice : Dice
{
    public int Level { get; private set; }
    private int enemyLevel;

    //public void InitializeEnemy(int playerMaxRoll)
    //{
    //    // Dynamic level scaling
    //    enemyLevel = Random.Range(1, Mathf.Max(2, playerMaxRoll / 6));
    //    Level = enemyLevel;

    //    // Adjust roll range based on level
    //    int minRoll = Mathf.Max(1, playerMaxRoll / 2 + (enemyLevel - 1) * 2);
    //    int maxRoll = Mathf.Min(playerMaxRoll + 10, playerMaxRoll + enemyLevel * 5);

    //    SetRollRange(minRoll, maxRoll);
    //    Debug.Log($"Enemy Dice Initialized with Level: {enemyLevel}, Range: {MinRoll}-{MaxRoll}");
    //}

    public void InitializeEnemy(int playerMaxRoll)
    {
        // Assign enemy level based on probabilities
        int randomValue = Random.Range(1,101);
        if (randomValue <= 40f) 
        {
            enemyLevel = 0; // 40% chance
        }
        else if (randomValue <= 70) 
        {
            enemyLevel = 1; // 30% chance
        }
        else if (randomValue <= 90) 
        {
            enemyLevel = 2; // 20% chance
        }
        else
        {
            enemyLevel = 3; // 10% chance
        }

        Level = enemyLevel;

        int minRoll = 1;
        int maxRoll = Mathf.Max(playerMaxRoll, 8);

        if (enemyLevel > 0)
        {
            maxRoll = playerMaxRoll + Mathf.FloorToInt(enemyLevel * 2.5f);
        }

        SetRollRange(minRoll, maxRoll);
        Debug.Log($"Enemy Dice Initialized with Level: {enemyLevel}, Range: {MinRoll}-{MaxRoll}");
    }

    public int GetMaxRoll()
    {
        return MaxRoll;
    }
}
