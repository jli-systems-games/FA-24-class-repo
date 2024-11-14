using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;
    private DiceRoller diceRoller;
    private List<EnemyRoller> activeEnemies = new List<EnemyRoller>();
    private EnemyRoller enemyRoller;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int spawnCount = 5;
    private Vector2 spawnAreaSize = new Vector2(14f, 8f);
    public float maxEnemyDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        diceRoller = GetComponent<DiceRoller>();

        SpawnNewEnemy(true);
        while (activeEnemies.Count < spawnCount)
        {
            SpawnNewEnemy(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnsureInRange();

        if (enemyRoller != null && Input.GetKeyDown(KeyCode.E))
        {
            StartEncounter(enemyRoller); 
        }
    }

    #region Manage Encounter

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyRoller enemy = other.GetComponent<EnemyRoller>();
            if (enemy != null)
            {
                enemyRoller = enemy;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyRoller enemy = other.GetComponent<EnemyRoller>();
            if (enemy != null && enemy == enemyRoller)
            {
                enemyRoller = null;
            }
        }
    }

    private void StartEncounter(EnemyRoller enemyRoller)
    {
        if (enemyRoller != null)
        {
            diceRoller.RollPlayer();
            enemyRoller.RollEnemy();
            int enemyRoll = enemyRoller.ResultEnemy();
            int playerRoll = diceRoller.ResultPlayer();

            ResolveEncounter(playerRoll, enemyRoll, enemyRoller);
        }
    }

    private void ResolveEncounter(int playerRoll, int enemyRoll, EnemyRoller enemyRoller)
    {
        if (playerRoll > enemyRoll)
        {
            diceRoller.IncreaseMaxPlayerRoll(enemyRoll);
            Destroy(enemyRoller.gameObject);
            activeEnemies.Remove(enemyRoller);
            SpawnNewEnemy(true);
        }
        else
        {
            diceRoller.DecreaseMaxPlayerRoll(enemyRoll);
            Destroy(enemyRoller.gameObject);
            activeEnemies.Remove(enemyRoller);
            SpawnNewEnemy(true);
        }
    }

    #endregion

    #region Spawn Enemy

    private void SpawnNewEnemy(bool inRange = false)
    {
        if (activeEnemies.Count >= spawnCount)  // Ensure only a maximum number of enemies are on screen
        {
            return;
        }

        Vector2 spawnPosition = new Vector2(
            spawnPoint.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnPoint.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        // Ensure enemies do not spawn too close to the player
        while (Vector2.Distance(spawnPosition, (Vector2)spawnPoint.position) < 1f)
        {
            spawnPosition = new Vector2(
                spawnPoint.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnPoint.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );
        }

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyRoller enemyComponent = newEnemy.GetComponent<EnemyRoller>();

        if (enemyComponent != null)
        {
            int playerMaxRoll = diceRoller.GetMaxPlayerRoll();

            // Initialize enemy with appropriate range restrictions based on player max roll
            enemyComponent.InitializeEnemy(playerMaxRoll, inRange);

            activeEnemies.Add(enemyComponent);
        }
    }

    private void EnsureInRange()
    {
        int maxPlayerRoll = diceRoller.GetMaxPlayerRoll();
        bool enemyInRange = false;

        foreach (EnemyRoller enemy in activeEnemies)
        {
            int maxEnemyRoll = enemy.GetMaxEnemyRoll();

            if (maxEnemyRoll <= maxPlayerRoll)
            {
                enemyInRange = true;
                break;
            }
        }

        if (!enemyInRange)
        {
            SpawnNewEnemy(true);
        }
    }

    #endregion
}
