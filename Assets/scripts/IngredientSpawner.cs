using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public GameObject lettucePrefab;
    public GameObject meatPrefab;
    public GameObject peppersPrefab;

    private GameObject cheeseInstance;
    private GameObject lettuceInstance;
    private GameObject meatInstance;
    private GameObject peppersInstance;

    public void SpawnCheese()
    {
        if (cheeseInstance == null)
        {
            cheeseInstance = Instantiate(cheesePrefab, new Vector2(0, 0), Quaternion.identity); // controls where prefab spawns
            cheeseInstance.AddComponent<MouseDrag>(); 
        }
    }

    public void SpawnLettuce()
    {
        if (lettuceInstance == null)
        {
            lettuceInstance = Instantiate(lettucePrefab, new Vector2(1, 0), Quaternion.identity);
            lettuceInstance.AddComponent<MouseDrag>(); 
        }
    }

    public void SpawnBacon()
    {
        if (meatInstance == null)
        {
            meatInstance = Instantiate(meatPrefab, new Vector2(2, 0), Quaternion.identity);
            meatInstance.AddComponent<MouseDrag>(); 
        }
    }

    public void SpawnPeppers()
    {
        if (peppersInstance == null)
        {
            peppersInstance = Instantiate(peppersPrefab, new Vector2(3, 0), Quaternion.identity);
            peppersInstance.AddComponent<MouseDrag>();
        }
    }
}
