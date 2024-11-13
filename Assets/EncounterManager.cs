using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;
    private DiceRoller diceRoller;
    private EnemyRoller enemyRoller;

    public GameObject enemyPrefab;
    private Transform spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        diceRoller = GetComponent<DiceRoller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRoller != null && Input.GetKeyDown(KeyCode.E))
        {
            StartEncounter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyRoller enemy = other.GetComponent<EnemyRoller>();
            if (enemy != null)
            {
                enemyRoller = enemy;
                Debug.Log("enemy in range");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemyRoller != null)
        {
            enemyRoller = null;
            Debug.Log("enemy leaving range");
        }
    }

    private void StartEncounter()
    {
        if (enemyRoller != null)
        {
            Debug.Log("Encounter started with enemy!");

            diceRoller.RollPlayer();
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
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemyRoller = newEnemy.GetComponent<EnemyRoller>();
    }
}
