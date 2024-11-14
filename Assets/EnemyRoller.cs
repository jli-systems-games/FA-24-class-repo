using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoller : MonoBehaviour
{
    public int enemyLevel;
    public int enemyRoll;
    public int minEnemyRoll = 1;
    public int maxEnemyRoll = 6;

    [SerializeField] private int maxEnemyLevel = 5;

    void Start()
    {
        SetEnemyRange();
    }

    public void SetEnemyRange()
    {
        enemyLevel = Random.Range(1, maxEnemyLevel + 1);
        maxEnemyRoll = 6 + (enemyLevel * enemyLevel) * 2;
    }

    public void RollEnemy()
    {
        enemyRoll = Random.Range(minEnemyRoll, maxEnemyRoll + 1);
        Debug.Log("Enemy score = " + enemyRoll);
    }

    public int ResultEnemy()
    {
        return enemyRoll;
    }

    public int GetMaxEnemyRoll()
    {
        return maxEnemyRoll;
    }

    public void SetMaxRollWithinRange(int playerMaxRoll)
    {
        maxEnemyRoll = Mathf.Min(playerMaxRoll, maxEnemyRoll);
    }
}

