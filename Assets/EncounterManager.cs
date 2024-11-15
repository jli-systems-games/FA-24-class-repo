using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;
    private DiceRoller diceRoller;
    private List<EnemyRoller> activeEnemies = new List<EnemyRoller>();
    private EnemyRoller enemyRoller;

    private RollAnimation playerRollAnimation;
    private RollAnimation enemyRollAnimation;
    private bool encounterReadyToResolve = false;
    public bool isDisplayingFinalRoll = false;

    public RollAnimation rollAnimation;

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
        rollAnimation = FindObjectOfType<RollAnimation>();

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
            if (!encounterReadyToResolve)
            {
                StartEncounter(enemyRoller);
            }
            else
            {
                ResolveEncounter(diceRoller.ResultPlayer(), enemyRoller.ResultEnemy(), enemyRoller);
                encounterReadyToResolve = false;  // Reset flag for next encounter
            }
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
            playerRollAnimation = diceRoller.GetComponent<RollAnimation>();
            enemyRollAnimation = enemyRoller.GetComponent<RollAnimation>();

            diceRoller.RollPlayer();
            enemyRoller.RollEnemy();
            int enemyRoll = enemyRoller.ResultEnemy();
            int playerRoll = diceRoller.ResultPlayer();

            // Set the direct reference to the final roll flag
            playerRollAnimation.isFinalRoll = true;
            enemyRollAnimation.isFinalRoll = true;

            encounterReadyToResolve = true;

            Debug.Log("EC: displaying final roll, setting flag to true");
        }
    }

    private void ResolveEncounter(int playerRoll, int enemyRoll, EnemyRoller enemyRoller)
    {
        if (playerRoll > enemyRoll)
        {
            diceRoller.IncreaseMaxPlayerRoll(enemyRoll);
        }
        else
        {
            diceRoller.DecreaseMaxPlayerRoll(enemyRoll);
        }

        // Destroy enemy and spawn a new one
        Destroy(enemyRoller.gameObject);
        activeEnemies.Remove(enemyRoller);
        SpawnNewEnemy(true);

        // Reset the final roll flag after resolving
        playerRollAnimation.isFinalRoll = false;
        enemyRollAnimation.isFinalRoll = false;
        isDisplayingFinalRoll = false;
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
