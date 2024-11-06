using UnityEngine;

public class WeedSpawner : MonoBehaviour
{
    public GameObject[] weedPrefabs; // 杂草Prefab数组
    public int weedCount = 10; // 杂草数量
    public Vector3 spawnAreaSize; // 生成范围大小

    void Start()
    {
        SpawnWeeds();
    }

    private void SpawnWeeds()
    {
        for (int i = 0; i < weedCount; i++)
        {
            // 随机选择一个杂草Prefab
            GameObject selectedWeed = weedPrefabs[Random.Range(0, weedPrefabs.Length)];

            // 生成随机位置
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0, // 假设杂草在地面上
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // 生成杂草
            Instantiate(selectedWeed, randomPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        // 可视化生成范围
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
