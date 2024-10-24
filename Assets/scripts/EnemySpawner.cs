using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ����Prefab
    public int totalEnemies; // ���ɵĵ�������
    public Vector3 spawnAreaMin; // �����������С��
    public Vector3 spawnAreaMax; // �������������
    public Animator doorAnimator; // �ŵĶ���������
    public string doorOpenAnimationName = "DoorOpen"; // ���Ŷ�������

    private int enemiesRemaining; // ��ǰ����ʣ��ĵ�����

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

    // ���ٵ�����
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
