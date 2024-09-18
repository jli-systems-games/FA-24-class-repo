using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinFall : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject diamoPrefab; // diamo 物体的预制体
    public float spawnRadius = 10f;
    public Transform playerTransform;

    public Material newMaterial; // 第一套新材质
    public Material secondNewMaterial; // 第二套新材质
    public Material originalMaterial; // 原始材质

    public GameObject[] textObjects1; // 第一套文字物体
    public GameObject[] textObjects2; // 第二套文字物体
    public GameObject[] textObjects3; // 第三套文字物体

    public GameObject MainCamera;
    public GameObject TransCamera;

    private int spawnCount = 0; // 记录生成的物体数量
    private int clickCount = 0; // 记录点击次数
    private List<GameObject> spawnedObjects = new List<GameObject>(); // 存储生成的硬币
    private List<GameObject> spawnedDiamoObjects = new List<GameObject>(); // 存储生成的 diamo
    private bool canSpawn = true; // 控制是否可以继续生成硬币
    private bool canSpawnDiamo = false; // 控制是否可以生成 diamo

    void Update()
    {
        // 检测按键是否被按下（M、K、L中的任意一个）
        if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && canSpawn)
        {
            SpawnObject();

            // 当可以生成 diamo 时，记录点击次数
            if (canSpawnDiamo)
            {
                clickCount++;

                // 每 10 次点击生成一个 diamo 物体
                if (clickCount % 5 == 0)
                {
                    SpawnDiamo();
                }
            }
        }
    }

    void SpawnObject()
    {
        if (objectPrefab != null && playerTransform != null && canSpawn)
        {
            // 生成硬币物体
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = playerTransform.position + randomOffset;

            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(newObject);
            spawnCount++; // 增加计数

            // 当生成的物体达到50个时，启动协程进行第一次摄像机转场和物体切换
            if (spawnCount == 50)
            {
                StartCoroutine(HandleTransition(textObjects1, textObjects2, newMaterial));
            }

            // 当生成的物体达到100个时，启动协程进行第二次摄像机转场和物体切换
            if (spawnCount == 100)
            {
                StartCoroutine(HandleTransition(textObjects2, textObjects3, secondNewMaterial));
            }

            // 当生成的物体达到110个时，启动协程进行清理和恢复操作
            if (spawnCount == 150)
            {
                StartCoroutine(HandleFinalTransition());
                canSpawn = false; // 停止生成更多硬币
            }
        }
        else
        {
            Debug.LogError("objectPrefab 或 playerTransform 未设置！");
        }
    }

    void SpawnDiamo()
    {
        if (diamoPrefab != null && playerTransform != null)
        {
            // 生成 diamo 物体
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = playerTransform.position + randomOffset;

            GameObject newDiamo = Instantiate(diamoPrefab, spawnPosition, Quaternion.identity);
            spawnedDiamoObjects.Add(newDiamo);
            Debug.Log("生成了一个 diamo 物体！");
        }
        else
        {
            Debug.LogError("diamoPrefab 或 playerTransform 未设置！");
        }
    }

    IEnumerator HandleTransition(GameObject[] oldTextObjects, GameObject[] newTextObjects, Material material)
    {
        Debug.Log("开始摄像机转场");

        // 切换到转场摄像机
        MainCamera.SetActive(false);
        TransCamera.SetActive(true);

        // 等待1秒钟后进行材质和文字物体的切换
        yield return new WaitForSeconds(1f);
        yield return null; // 确保摄像机切换

        // 执行材质和文字物体的切换
        ChangeMaterialOfObjects(material);
        SwitchTextObjects(oldTextObjects, newTextObjects);

        // 切换后开始允许生成 diamo
        canSpawnDiamo = true;

        // 等待3秒钟后切换回主摄像机
        yield return new WaitForSeconds(3f);
        yield return null; // 确保渲染更新

        // 切换回主摄像机
        TransCamera.SetActive(false);
        MainCamera.SetActive(true);

        Debug.Log("摄像机转场完成");
    }

    IEnumerator HandleFinalTransition()
    {
        Debug.Log("开始最终摄像机转场");

        // 切换到转场摄像机
        MainCamera.SetActive(false);
        TransCamera.SetActive(true);

        // 等待1秒钟后进行清理和恢复操作
        yield return new WaitForSeconds(1f);
        yield return null; // 确保摄像机切换

        // 清空生成出的硬币和 diamo
        ClearSpawnedObjects();
        ClearSpawnedDiamoObjects();

        // 恢复物体材质和文字颜色
        RestoreMaterialOfObjects();
        RestoreTextObjects();

        // 等待3秒钟后切换回主摄像机
        yield return new WaitForSeconds(3f);
        yield return null; // 确保渲染更新

        // 切换回主摄像机
        TransCamera.SetActive(false);
        MainCamera.SetActive(true);

        Debug.Log("最终摄像机转场完成");
    }

    void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear(); // 清空列表
    }

    void ClearSpawnedDiamoObjects()
    {
        foreach (GameObject obj in spawnedDiamoObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedDiamoObjects.Clear(); // 清空 diamo 列表
    }

    void ChangeMaterialOfObjects(Material material)
    {
        GameObject objM = GameObject.Find("M");
        GameObject objK = GameObject.Find("K");
        GameObject objL = GameObject.Find("L");

        ChangeMaterial(objM, "M", material);
        ChangeMaterial(objK, "K", material);
        ChangeMaterial(objL, "L", material);
    }

    void ChangeMaterial(GameObject obj, string name, Material material)
    {
        if (obj != null)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material = material;
                Debug.Log($"物体{name}的材质已成功替换！");
            }
            else
            {
                Debug.LogError($"物体{name}没有Renderer组件！");
            }
        }
        else
        {
            Debug.LogError($"未找到名为{name}的物体！");
        }
    }

    void SwitchTextObjects(GameObject[] oldTextObjects, GameObject[] newTextObjects)
    {
        foreach (GameObject textObject in oldTextObjects)
        {
            if (textObject != null)
            {
                textObject.SetActive(false);
            }
        }

        foreach (GameObject textObject in newTextObjects)
        {
            if (textObject != null)
            {
                textObject.SetActive(true);
            }
        }
    }

    void RestoreMaterialOfObjects()
    {
        GameObject objM = GameObject.Find("M");
        GameObject objK = GameObject.Find("K");
        GameObject objL = GameObject.Find("L");

        ChangeMaterial(objM, "M", originalMaterial);
        ChangeMaterial(objK, "K", originalMaterial);
        ChangeMaterial(objL, "L", originalMaterial);
    }

    void RestoreTextObjects()
    {
        foreach (GameObject textObject in textObjects1)
        {
            if (textObject != null)
            {
                textObject.SetActive(true);
            }
        }
        foreach (GameObject textObject in textObjects2)
        {
            if (textObject != null)
            {
                textObject.SetActive(false);
            }
        }
        foreach (GameObject textObject in textObjects3)
        {
            if (textObject != null)
            {
                textObject.SetActive(false);
            }
        }
    }
}
