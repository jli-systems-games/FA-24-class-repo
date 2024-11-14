using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;
    private DiceRoller diceRoller;
    private EnemyRoller enemyRoller;

    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int spawnCount = 4; 
    public Vector2 spawnAreaSize = new Vector2(14f, 8f);

    void Start()
    {
        diceRoller = GetComponent<DiceRoller>();

        // Spawn initial set of enemies
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnNewEnemy();
        }
    }

    void Update()
    {
        if (enemyRoller != null && Input.GetKeyDown(KeyCode.E))
        {
            StartEncounter();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyRoller enemy = other.GetComponent<EnemyRoller>();
            if (enemy != null)
            {
                enemyRoller = enemy;
                Debug.Log("Enemy in range");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && enemyRoller != null && other.GetComponent<EnemyRoller>() == enemyRoller)
        {
            enemyRoller = null;
            Debug.Log("Enemy out of range");
        }
    }

    private void StartEncounter()
    {
        if (enemyRoller != null)
        {
            Debug.Log("Encounter started with enemy!");

            diceRoller.RollPlayer();
            enemyRoller.RollEnemy();
            int enemyRoll = enemyRoller.ResultEnemy();
            int playerRoll = diceRoller.ResultPlayer();

            ResolveEncounter(playerRoll, enemyRoll);
        }
    }

    private void ResolveEncounter(int playerRoll, int enemyRoll)
    {
        if (playerRoll > enemyRoll)
        {
            diceRoller.IncreaseMaxPlayerRoll(enemyRoll);
            Destroy(enemyRoller.gameObject);
            SpawnNewEnemy();
        }
        else
        {
            diceRoller.DecreaseMaxPlayerRoll(enemyRoll);
        }
    }

    private void SpawnNewEnemy()
    {
        // Calculate a random position within the spawn area
        Vector2 randomPosition = new Vector2(
            spawnPoint.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnPoint.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        // Instantiate enemy at the random position
        GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        newEnemy.GetComponent<EnemyRoller>(); // Access and configure if needed
    }
}
