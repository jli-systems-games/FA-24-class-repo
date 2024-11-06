using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject[] prefabsZone1; // z < -10
    public GameObject[] prefabsZone2; // -10 <= z <= 10
    public GameObject[] prefabsZone3; // z > 10
    public Transform player;
    public float spawnRadius = 10f;
    public int maxObjects = 5;
    public float checkInterval = 1f;
    public float spawnHeightOffset = -2f;
    public float animationDuration = 1f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating("ManageObjects", 0f, checkInterval);
    }

    private void ManageObjects()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(player.position, spawnedObjects[i].transform.position) > spawnRadius)
            {
                StartCoroutine(DelayedDestroy(spawnedObjects[i], 1f));
                spawnedObjects.RemoveAt(i);
            }
        }

        while (spawnedObjects.Count < maxObjects)
        {
            SpawnObjectAroundPlayer();
        }
    }

    private void SpawnObjectAroundPlayer()
    {
        
        GameObject[] selectedPrefabs;
        if (player.position.z < -10)
        {
            selectedPrefabs = prefabsZone1;
        }
        else if (player.position.z > 10)
        {
            selectedPrefabs = prefabsZone3;
        }
        else
        {
            selectedPrefabs = prefabsZone2;
        }

       
        GameObject prefab = selectedPrefabs[Random.Range(0, selectedPrefabs.Length)];
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(player.position.x + randomPoint.x, player.position.y, player.position.z + randomPoint.y);

        GameObject spawnedObject = Instantiate(prefab, spawnPosition + Vector3.up * spawnHeightOffset, Quaternion.identity);
        spawnedObjects.Add(spawnedObject);

        StartCoroutine(AnimateSpawn(spawnedObject, spawnPosition));
    }

    private IEnumerator AnimateSpawn(GameObject obj, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;

        while (elapsedTime < animationDuration)
        {
            if (obj == null) yield break;

            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (obj != null)
        {
            obj.transform.position = targetPosition;
        }
    }

    private IEnumerator DelayedDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
