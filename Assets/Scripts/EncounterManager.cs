using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;
    public enum EncounterState { Idle, Rolling, Resolving }
    private EncounterState currentState = EncounterState.Idle;

    [Header("Player and Enemy Reference")]
    public PlayerDice playerDice; // Assign the PlayerDice object here
    private List<EnemyDice> activeEnemies = new List<EnemyDice>();
    private EnemyDice currentEnemyDice;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab; // Assign the enemy prefab here
    public Transform spawnPoint;
    public int spawnCount = 5;
    private Vector2 spawnAreaSize = new Vector2(14f, 8f);

    private void Start()
    {
        playerDice.SetRollRange(1, 6);
        SpawnEnemies();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EncounterState.Idle:
                if (Input.GetKeyDown(KeyCode.E) && currentEnemyDice != null)
                {
                    StartRolling();
                }
                break;

            case EncounterState.Rolling:
                // Displaying dice rolls; wait for transition to resolving state
                break;

            case EncounterState.Resolving:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ResolveEncounter();
                }
                break;
        }
    }

    #region Encounter Logic

    private void StartRolling()
    {
        if (currentState == EncounterState.Idle && currentEnemyDice != null)
        {
            playerDice.Roll();
            currentEnemyDice.Roll();

            Debug.Log($"Player Roll: {playerDice.GetRollResult()}, Enemy Roll: {currentEnemyDice.GetRollResult()}");
            currentState = EncounterState.Resolving;
        }
    }

    private void ResolveEncounter()
    {
        if (currentState == EncounterState.Resolving)
        {
            int playerRoll = playerDice.GetRollResult();
            int enemyRoll = currentEnemyDice.GetRollResult();

            if (playerRoll > enemyRoll)
            {
                Debug.Log("Player Wins!");
                playerDice.SetRollRange(playerDice.MinRoll, playerDice.MaxRoll + enemyRoll);
            }
            else
            {
                Debug.Log("Player Loses!");
                playerDice.SetRollRange(playerDice.MinRoll, Mathf.Max(playerDice.MinRoll, playerDice.MaxRoll - enemyRoll));
            }

            // Remove defeated enemy
            Destroy(currentEnemyDice.gameObject);
            activeEnemies.Remove(currentEnemyDice);
            currentEnemyDice = null;

            SpawnNewEnemy();

            currentState = EncounterState.Idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && currentState == EncounterState.Idle)
        {
            currentEnemyDice = collision.GetComponent<EnemyDice>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && currentEnemyDice != null && collision.GetComponent<EnemyDice>() == currentEnemyDice)
        {
            currentEnemyDice = null;
        }
    }

    #endregion

    #region Enemy Spawning

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnNewEnemy();
        }
    }

    private void SpawnNewEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            spawnPoint.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnPoint.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyDice enemyDice = newEnemy.GetComponent<EnemyDice>();
        if (enemyDice != null)
        {
            int playerMaxRoll = playerDice.MaxRoll;

            // Initialize enemy with random roll range based on the player's roll
            int enemyMinRoll = Mathf.Max(1, playerMaxRoll / 2);
            int enemyMaxRoll = Mathf.Min(playerMaxRoll + 5, playerMaxRoll * 2);
            enemyDice.SetRollRange(enemyMinRoll, enemyMaxRoll);

            activeEnemies.Add(enemyDice);
        }
    }

    #endregion
}
