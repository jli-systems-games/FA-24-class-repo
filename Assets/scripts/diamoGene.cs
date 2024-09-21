using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamoGene : MonoBehaviour
{
    public GameObject diamoPrefab; 
    public float spawnRadius = 10f;
    public Transform playerTransform;

    private int clickCount = 0; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            
            clickCount++;

            
            if (clickCount % 10 == 0)
            {
                SpawnDiamo();
            }
        }
    }

    
    void SpawnDiamo()
    {
        if (diamoPrefab != null && playerTransform != null)
        {
            
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
