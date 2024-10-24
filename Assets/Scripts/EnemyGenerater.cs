using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    public GameObject airEnemy;              
    public float initialSpawnInterval = 5f;  
    public int initialSpawnAmount = 2;       
    public float spawnIntervalDecay = 0.95f; 
    public int maxSpawnAmount = 10;
    private int spawnAmountIncrement = 1;   
    public float minSpawnInterval = 1f;      

    private float currentSpawnInterval;      
    private int currentSpawnAmount;          
    private bool shouldSpawn = true;         
    private GameManager gameManager;
    private GameObject playerTransform;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        playerTransform = gameManager.playerTransform;

        
        currentSpawnInterval = initialSpawnInterval;
        currentSpawnAmount = initialSpawnAmount;
        
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (shouldSpawn)
        {
            yield return new WaitForSeconds(currentSpawnInterval);

            SpawnAirEnemy(currentSpawnAmount);

            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval * spawnIntervalDecay); // 间隔时间变短

            if (currentSpawnAmount < maxSpawnAmount)
            {
                float chance = Random.Range(0f, 1f);
                if (chance < 0.3)
                    currentSpawnAmount += spawnAmountIncrement;
            }
        }
    }

    void SpawnAirEnemy(int times)
    {
        for (int i = 0; i < times; i++)
        {
            playerTransform = gameManager.playerTransform;

            float x = Random.Range(playerTransform.transform.position.x < 0 ? -10 : 8, playerTransform.transform.position.x < 0 ? -8 : 10);
            float y = Random.Range(10, 20);
            float z = 0;

            Vector3 spawnPosition = new Vector3(x, y, z);
            Instantiate(airEnemy, spawnPosition, Quaternion.identity);
        }
    }

    // 停止生成敌人，并销毁场景中的所有敌人
    public void StopSpawningAndKillEnemies()
    {        
        shouldSpawn = false;
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GameEndKill();
            }
        }
    }
}
