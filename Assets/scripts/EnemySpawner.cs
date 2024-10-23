using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ���˵�Prefab
    public int enemyCount = 10; // ���ɵĵ�������
    public Vector3 spawnAreaMin; // �������С����
    public Vector3 spawnAreaMax; // ������������

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // ��ָ���ķ�Χ���������λ��
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // ʵ��������
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
