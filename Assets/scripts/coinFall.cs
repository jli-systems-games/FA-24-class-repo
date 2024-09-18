using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinFall : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnRadius = 10f;
    public Transform playerTransform;

    public Material newMaterial; // 第一套新材质
    public Material secondNewMaterial; // 第二套新材质

    public GameObject[] textObjects1; // 第一套文字物体
    public GameObject[] textObjects2; // 第二套文字物体
    public GameObject[] textObjects3; // 第三套文字物体

    public GameObject MainCamera;
    public GameObject TransCamera;

    private int spawnCount = 0; // 记录生成的物体数量
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
            // 生成物体并增加计数
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = playerTransform.position + randomOffset;

            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(newObject);
            spawnCount++; // 增加计数

            // 当生成的物体达到20个时，启动协程进行第一次摄像机转场和物体切换
            if (spawnCount == 20)
            {
                StartCoroutine(HandleTransition(textObjects1, textObjects2, newMaterial));
            }

            // 当生成的物体达到40个时，启动协程进行第二次摄像机转场和物体切换
            if (spawnCount == 40)
            {
                StartCoroutine(HandleTransition(textObjects2, textObjects3, secondNewMaterial));
            }
        }
        else
        {
            Debug.LogError("objectPrefab 或 playerTransform 未设置！");
        }
    }

    IEnumerator HandleTransition(GameObject[] oldTextObjects, GameObject[] newTextObjects, Material material)
    {
        // 切换到转场摄像机
        MainCamera.SetActive(false);
        TransCamera.SetActive(true);

        // 等待1秒钟后进行材质和文字物体的切换
        yield return new WaitForSeconds(1f);

        // 执行材质和文字物体的切换
        ChangeMaterialOfObjects(material);
        SwitchTextObjects(oldTextObjects, newTextObjects);

        // 等待5秒钟后切换回主摄像机
        yield return new WaitForSeconds(5f);

        // 切换回主摄像机
        TransCamera.SetActive(false);
        MainCamera.SetActive(true);
    }

    void ChangeMaterialOfObjects(Material material)
    {
        // 分别查找场景中的物体名为"M", "K", "L"
        GameObject objM = GameObject.Find("M");
        GameObject objK = GameObject.Find("K");
        GameObject objL = GameObject.Find("L");

        // 替换每个物体的材质
        ChangeMaterial(objM, "M", material);
        ChangeMaterial(objK, "K", material);
        ChangeMaterial(objL, "L", material);
    }

    void ChangeMaterial(GameObject obj, string name, Material material)
    {
        if (obj != null)
        {
            // 获取物体的Renderer组件
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                // 替换材质
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
        // 关闭旧的文字物体
        foreach (GameObject textObject in oldTextObjects)
        {
            if (textObject != null)
            {
                textObject.SetActive(false);
            }
        }

        // 打开新的文字物体
        foreach (GameObject textObject in newTextObjects)
        {
            if (textObject != null)
            {
                textObject.SetActive(true);
            }
        }
    }
}
