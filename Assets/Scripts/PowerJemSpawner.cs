using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerJemSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // 可生成的物品Prefab数组
    private float spawnInterval = 2.5f; // 生成物品的时间间隔
    private float minSpawnInterval = 0.5f; // 最小生成间隔
    private float maxSpawnInterval = 2.5f; // 最大生成间隔
    private float accelerationDuration = 10f; // 加速时长
    private SpriteRenderer spriteRenderer; // 获取当前挂载的Sprite的范围
    private float timer;
    private float overAllTimer;
    private List<GameObject> spawnedItems; // 存储生成的物品列表

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = spawnInterval;
        overAllTimer = 0f;
        spawnedItems = new List<GameObject>();
    }

    void Update()
    {
        // 更新 overall timer
        overAllTimer += Time.deltaTime;

        // 根据 overAllTimer 动态调整 spawnInterval，10秒内逐渐减小到 0.5 秒
        spawnInterval = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, overAllTimer / accelerationDuration);

        // 确保 spawnInterval 不低于 minSpawnInterval
        spawnInterval = Mathf.Clamp(spawnInterval, minSpawnInterval, maxSpawnInterval);

        // 减少生成物品的计时器
        timer -= Time.deltaTime;

        // 当计时器小于等于0时，生成一个新物品并重置计时器
        if (timer <= 0)
        {
            SpawnRandomItem();
            timer = spawnInterval;
        }
    }

    void SpawnRandomItem()
    {
        if (prefabs.Length == 0) return;

        // 获取sprite的边界
        Bounds bounds = spriteRenderer.bounds;

        // 在X和Y轴上随机选择一个点
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // 随机选择一个Prefab
        int randomIndex = Random.Range(0, prefabs.Length);

        // 在随机位置生成物品
        GameObject newItem = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);

        // 将生成的物品添加到列表中
        spawnedItems.Add(newItem);
    }

    // 清空已生成的物品
    public void ClearSpawnedItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                Destroy(item); // 销毁生成的物品
            }
        }

        // 清空列表
        spawnedItems.Clear();

        // 重置 overall timer
        overAllTimer = 0f;
    }
}
