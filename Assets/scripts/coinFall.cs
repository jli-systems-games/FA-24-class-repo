using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinFall : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject diamoPrefab;
    public float spawnRadius = 10f;
    public Transform playerTransform;

    public Material newMaterial;
    public Material secondNewMaterial;
    public Material originalMaterial;

    public GameObject[] textObjects1;
    public GameObject[] textObjects2;
    public GameObject[] textObjects3;

    public GameObject MainCamera;
    public GameObject TransCamera;

    public AudioSource hitTableAudioSource; 
    public List<AudioClip> coinSoundList; 

    private AudioSource coinAudioSource;
    private int spawnCount = 0;
    private int clickCount = 0;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> spawnedDiamoObjects = new List<GameObject>();
    private bool canSpawn = true;
    private bool canSpawnDiamo = false;

    
    void Start()
    {
        if (hitTableAudioSource != null)
        {
            hitTableAudioSource.enabled = false;
        }

        
        coinAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && canSpawn)
        {
            SpawnObject();

            if (canSpawnDiamo)
            {
                clickCount++;

                if (clickCount % 5 == 0)
                {
                    SpawnDiamo();
                }
            }

           
            PlayRandomCoinSound();
        }
    }

    void SpawnObject()
    {
        if (objectPrefab != null && playerTransform != null && canSpawn)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = playerTransform.position + randomOffset;

            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(newObject);
            spawnCount++;

            if (spawnCount == 50)
            {
                StartCoroutine(HandleTransition(textObjects1, textObjects2, newMaterial));
            }

            if (spawnCount == 150)
            {
                StartCoroutine(HandleTransition(textObjects2, textObjects3, secondNewMaterial));
            }

            if (spawnCount == 350)
            {
                StartCoroutine(HandleFinalTransition());
                canSpawn = false;
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

        
        if (hitTableAudioSource != null)
        {
            hitTableAudioSource.enabled = true;
            hitTableAudioSource.Play();
        }

        MainCamera.SetActive(false);
        TransCamera.SetActive(true);

        yield return new WaitForSeconds(1f);
        yield return null;

        ChangeMaterialOfObjects(material);
        SwitchTextObjects(oldTextObjects, newTextObjects);

        canSpawnDiamo = true;

        yield return new WaitForSeconds(3f);
        yield return null;

        
        if (hitTableAudioSource != null)
        {
            hitTableAudioSource.enabled = false;
        }

        TransCamera.SetActive(false);
        MainCamera.SetActive(true);

        Debug.Log("摄像机转场完成");
    }

    IEnumerator HandleFinalTransition()
    {
        Debug.Log("开始最终摄像机转场");

        
        if (hitTableAudioSource != null)
        {
            hitTableAudioSource.enabled = true;
            hitTableAudioSource.Play();
        }

        MainCamera.SetActive(false);
        TransCamera.SetActive(true);

        yield return new WaitForSeconds(1f);
        yield return null;

        ClearSpawnedObjects();
        ClearSpawnedDiamoObjects();
        StopCoinSound(); 

        RestoreMaterialOfObjects();
        RestoreTextObjects();

        yield return new WaitForSeconds(3f);
        yield return null;

       
        if (hitTableAudioSource != null)
        {
            hitTableAudioSource.enabled = false;
        }

        TransCamera.SetActive(false);
        MainCamera.SetActive(true);

        Debug.Log("最终摄像机转场完成");
    }

    void PlayRandomCoinSound()
    {
        if (coinSoundList != null && coinSoundList.Count > 0)
        {
            int randomIndex = Random.Range(0, coinSoundList.Count);
            AudioClip randomClip = coinSoundList[randomIndex];
            if (randomClip != null)
            {
                
                coinAudioSource.clip = randomClip;
                coinAudioSource.Play();
            }
        }
    }

    void StopCoinSound()
    {
        if (coinAudioSource.isPlaying)
        {
            coinAudioSource.Stop();
        }
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
        spawnedObjects.Clear();
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
        spawnedDiamoObjects.Clear();
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
