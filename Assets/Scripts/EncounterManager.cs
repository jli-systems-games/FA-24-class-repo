using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;
    public enum EncounterState { Idle, Rolling, Resolving }
    public EncounterState currentState = EncounterState.Idle;

    [Header("Player and Enemy Reference")]
    public PlayerDice playerDice; // Assign the PlayerDice object here
    private List<EnemyDice> activeEnemies = new List<EnemyDice>();
    private EnemyDice currentEnemyDice;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab; // Assign the enemy prefab here
    public Transform spawnPoint;
    public int spawnCount = 5;
    private Vector2 spawnAreaSize = new Vector2(14f, 8f);

    private RollAnimation playerRollAnimation;
    private RollAnimation enemyRollAnimation;
    public TextMeshProUGUI uiText;

    private void Start()
    {
        instance = this;  // Singleton setup
        playerDice.SetRollRange(1, 6);
        SpawnEnemies();

        // Initialize roll animations
        playerRollAnimation = playerDice.GetComponent<RollAnimation>();
        if (currentEnemyDice != null)
        {
            enemyRollAnimation = currentEnemyDice.GetComponent<RollAnimation>();
        }
    }

    private void Update()
    {
        // Check if the current state allows for transitioning to Rolling state
        switch (currentState)
        {
            case EncounterState.Idle:
                if (Input.GetKeyDown(KeyCode.E) && currentEnemyDice != null)
                {
                    StartEncounter();
                }
                break;

            case EncounterState.Rolling:
                break;

            case EncounterState.Resolving:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ResolveEncounter();
                }
                break;
        }
    }

    private void StartEncounter()
    {
        currentState = EncounterState.Rolling;

        // Start roll animations immediately
        if (playerRollAnimation != null)
            StartCoroutine(playerRollAnimation.PlayRollAnimation(2f));

        if (enemyRollAnimation != null)
            StartCoroutine(enemyRollAnimation.PlayRollAnimation(2f));

        // Perform rolls after the animation
        StartCoroutine(PerformRolls());
    }

    private IEnumerator PerformRolls()
    {
        // Wait for the roll animations to finish
        yield return new WaitForSeconds(2f);

        // Perform rolls
        playerDice.Roll();
        currentEnemyDice.Roll();
        uiText.text = "press E to exit";

        // Transition to Resolving state
        currentState = EncounterState.Resolving;
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
                playerDice.SetRollRange(playerDice.MinRoll, Mathf.Max(6, playerDice.MaxRoll - enemyRoll / 2));
            }

            // Remove defeated enemy
            Destroy(currentEnemyDice.gameObject);
            activeEnemies.Remove(currentEnemyDice);
            currentEnemyDice = null;

            SpawnNewEnemy();
            currentState = EncounterState.Idle;
            uiText.text = "";
            playerRollAnimation.isFinalRoll = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && currentState == EncounterState.Idle)
        {
            uiText.text = "press E to attack";
            currentEnemyDice = collision.GetComponent<EnemyDice>();
            // Update enemy roll animation reference
            enemyRollAnimation = currentEnemyDice.GetComponent<RollAnimation>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && currentEnemyDice != null && collision.GetComponent<EnemyDice>() == currentEnemyDice)
        {
            uiText.text = "";
            currentEnemyDice = null;
        }
    }

    #region Enemy Spawning

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnNewEnemy();
        }
    }

    //private void SpawnNewEnemy()
    //{
    //    Vector2 spawnPosition = new Vector2(
    //        spawnPoint.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
    //        spawnPoint.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
    //    );

    //    GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //    EnemyDice enemyDice = newEnemy.GetComponent<EnemyDice>();
    //    if (enemyDice != null)
    //    {
    //        // Initialize the enemy's roll range based on the player's max roll
    //        enemyDice.InitializeEnemy(playerDice.MaxRoll);

    //        // Add the enemy to the active list
    //        activeEnemies.Add(enemyDice);
    //    }
    //}

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
            // Initialize enemy based on player's max roll
            enemyDice.InitializeEnemy(playerDice.MaxRoll);

            // Add the enemy to the active list
            activeEnemies.Add(enemyDice);
        }
    }


    #endregion
}
