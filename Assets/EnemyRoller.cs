using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoller : MonoBehaviour
{
    public int enemyLevel;
    public int enemyRoll;
    private int minEnemyRoll = 1;
    private int maxEnemyRoll = 6;

    [SerializeField] private int maxEnemyLevel = 5;

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyRange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetEnemyRange()
    {
        enemyLevel = Random.Range(1, maxEnemyLevel + 1);
        maxEnemyRoll = 6 + (enemyLevel * enemyLevel) * 2;

    }

    public void RollEnemy()
    {
        enemyRoll = Random.Range(minEnemyRoll, maxEnemyRoll + 1);
        Debug.Log("enemy score = " + enemyRoll);
    }

    public int ResultEnemy()
    {
        return enemyRoll;
    }
}
