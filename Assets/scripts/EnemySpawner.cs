using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人的Prefab
    public int enemyCount = 10; // 生成的敌人数量
    public Vector3 spawnAreaMin; // 区域的最小坐标
    public Vector3 spawnAreaMax; // 区域的最大坐标

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 在指定的范围内随机生成位置
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // 实例化敌人
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
