using System.Collections.Generic;
using UnityEngine;

public class coinFall : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnRadius = 10f;
    public Transform playerTransform;
    public int maxObjects = 50; // 最大生成物体数量

    private List<GameObject> spawnedObjects = new List<GameObject>(); // 存储生成的物体

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        if (objectPrefab != null && playerTransform != null)
        {
            // 当生成的物体数量达到上限时，清空它们
            if (spawnedObjects.Count >= maxObjects)
            {
                ClearSpawnedObjects();
            }

            // 生成新的物体
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = playerTransform.position + randomOffset;

            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(newObject); // 将新生成的物体添加到列表中
        }
        else
        {
            Debug.LogError("objectPrefab 或 playerTransform 未设置！");
        }
    }

    void ClearSpawnedObjects()
    {
        // 遍历并销毁所有生成的物体
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }

        // 清空列表
        spawnedObjects.Clear();
    }
}
