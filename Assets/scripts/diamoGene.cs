using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamoGene : MonoBehaviour
{
    public GameObject diamoPrefab; // diamo物体的预制体
    public float spawnRadius = 10f;
    public Transform playerTransform;

    private int clickCount = 0; // 记录玩家按键次数

    void Update()
    {
        // 检测按键是否被按下（M、K、L中的任意一个）
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            // 增加按键点击计数
            clickCount++;

            // 每检测到5次点击时生成一个diamo物体
            if (clickCount % 10 == 0)
            {
                SpawnDiamo();
            }
        }
    }

    // diamo物体生成逻辑
    void SpawnDiamo()
    {
        if (diamoPrefab != null && playerTransform != null)
        {
            // 生成 diamo 物体
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = playerTransform.position + randomOffset;

            Instantiate(diamoPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("生成了一个 diamo 物体！");
        }
        else
        {
            Debug.LogError("diamoPrefab 或 playerTransform 未设置！");
        }
    }
}
