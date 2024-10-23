using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public GameObject lettucePrefab;
    public GameObject baconPrefab;
    public GameObject peppersPrefab;
    public GameObject candyPrefab;
    public GameObject honeyPrefab;

    public void SpawnCheese()
    {
        GameObject cheeseInstance = Instantiate(cheesePrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f)), Quaternion.identity);
        cheeseInstance.AddComponent<MouseDrag>();
    }

    public void SpawnLettuce()
    {
        GameObject lettuceInstance = Instantiate(lettucePrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f)), Quaternion.identity);
        lettuceInstance.AddComponent<MouseDrag>();
    }

    public void SpawnBacon()
    {
        GameObject baconInstance = Instantiate(baconPrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f)), Quaternion.identity);
        baconInstance.AddComponent<MouseDrag>();
    }

    public void SpawnPeppers()
    {
        GameObject peppersInstance = Instantiate(peppersPrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f)), Quaternion.identity);
        peppersInstance.AddComponent<MouseDrag>();
    }

    public void SpawnCandy()
    {
        GameObject candyInstance = Instantiate(candyPrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f)), Quaternion.identity);
        candyInstance.AddComponent<MouseDrag>();
    }

    public void SpawnHoney()
    {
        GameObject honeyInstance = Instantiate(honeyPrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f)), Quaternion.identity);
        honeyInstance.AddComponent<MouseDrag>();
    }
}
