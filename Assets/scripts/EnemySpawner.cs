using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人Prefab
    public int totalEnemies; // 生成的敌人总数
    public Vector3 spawnAreaMin; // 生成区域的最小点
    public Vector3 spawnAreaMax; // 生成区域的最大点
    public Animator doorAnimator; // 门的动画控制器
    public string doorOpenAnimationName = "DoorOpen"; // 开门动画名字

    private int enemiesRemaining; // 当前场景剩余的敌人数

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        enemiesRemaining = totalEnemies;

        for (int i = 0; i < totalEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    // 减少敌人数
    public void EnemyCleared()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.Play(doorOpenAnimationName);
            Debug.Log("All enemies cleared. Door opened!");
        }
    }
}
