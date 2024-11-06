using UnityEngine;

public class WeedSpawner : MonoBehaviour
{
    public GameObject[] weedPrefabs; // 杂草Prefab数组
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // 生成范围大小
    public int initialWeedCount = 20; // 初始生成数量
    public int weedsPerInterval = 2; // 每次间隔生成数量
    public float spawnInterval = 10.0f; // 生成间隔（秒）
    public int maxWeeds = 100; // 最大杂草数量（可选）

    private int currentWeedCount = 0; // 当前场景中的杂草数量

    void Start()
    {
        // 初始生成
        SpawnWeeds(initialWeedCount);

        // 每 10 秒生成杂草
        InvokeRepeating(nameof(SpawnIntervalWeeds), spawnInterval, spawnInterval);
    }

    private void SpawnWeeds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (weedPrefabs.Length == 0)
            {
                Debug.LogWarning("没有配置杂草 Prefab！");
                return;
            }

            // 随机选择一个杂草Prefab
            GameObject selectedWeed = weedPrefabs[Random.Range(0, weedPrefabs.Length)];

            // 随机生成位置
            Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0, // 假设杂草在地面上
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // 检查是否达到最大杂草数量（可选）
            if (currentWeedCount >= maxWeeds)
            {
                Debug.Log("达到最大杂草数量，停止生成！");
                return;
            }

            // 生成杂草
            Instantiate(selectedWeed, randomPosition, Quaternion.identity);
            currentWeedCount++;
        }
    }

    private void SpawnIntervalWeeds()
    {
        // 每隔 spawnInterval 秒生成 weedsPerInterval 个杂草
        SpawnWeeds(weedsPerInterval);
    }

    private void OnDrawGizmos()
    {
        // 可视化生成范围
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
