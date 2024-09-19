using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public TextMeshProUGUI debugUI;
    public GameObject[] enemy;
    private bool[] isOnCooldown;
    public Transform spawnPoint;
    public float spawnInterval = 12f;
    public float maxSpawnInterval = 1f;
    public float accelerationDuration = 60f;

    private float timeElapsed = 0f;
    private Coroutine spawnCoroutine;
    private Coroutine accelerateCoroutine;

    void Start()
    {
        debugUI.gameObject.SetActive(false);
        isOnCooldown = new bool[enemy.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (debugUI.gameObject.activeSelf)
                debugUI.gameObject.SetActive(false);
            else
                debugUI.gameObject.SetActive(true);
        }

        debugUI.text = "Spawn interval: " + spawnInterval.ToString("F1") + "/s";
    }

    public void GameStart()
    {
        spawnInterval = 12f;
        timeElapsed = 0f;
        spawnCoroutine = StartCoroutine(SpawnEnemy());
        accelerateCoroutine = StartCoroutine(AccelerateSpawnRate());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TrySpawnEnemy();
        }
    }

    IEnumerator AccelerateSpawnRate()
    {
        while (timeElapsed < accelerationDuration)
        {
            timeElapsed += Time.deltaTime;
            spawnInterval = Mathf.Lerp(12f, maxSpawnInterval, timeElapsed / accelerationDuration);
            yield return null;
        }
        spawnInterval = maxSpawnInterval;
    }

    void TrySpawnEnemy()
    {
        var availableEnemies = new System.Collections.Generic.List<int>();

        for (int i = 0; i < enemy.Length; i++)
        {
            if (!isOnCooldown[i])
            {
                availableEnemies.Add(i);
            }
        }

        if (availableEnemies.Count > 0)
        {
            int randomIndex = availableEnemies[Random.Range(0, availableEnemies.Count)];
            Instantiate(enemy[randomIndex], spawnPoint.position, Quaternion.identity);
            StartCoroutine(Cooldown(randomIndex));
        }       
    }

    IEnumerator Cooldown(int index)
    {
        isOnCooldown[index] = true;
        yield return new WaitForSeconds(4f);
        isOnCooldown[index] = false;
    }

    public void EndGame()
    {
        // 停止所有正在运行的协程
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        if (accelerateCoroutine != null) StopCoroutine(accelerateCoroutine);
    }
}
