using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseIngredientSpawner : MonoBehaviour
{
    public GameObject pizzaBasePrefab;
    public GameObject sandwichBasePrefab;
    public GameObject hotdogBasePrefab;

    private bool baseSpawned = false;

    public void SpawnPizzaBase()
    {
        if (!baseSpawned) 
        {
            Instantiate(pizzaBasePrefab, new Vector2(-1, -1), Quaternion.identity);
            baseSpawned = true; 
        }
    }

    public void SpawnSandwichBase()
    {
        if (!baseSpawned) 
        {
            Instantiate(sandwichBasePrefab, new Vector2(-1, -1), Quaternion.identity); 
            baseSpawned = true; 
        }
    }

    public void SpawnHotdogBase()
    {
        if (!baseSpawned) 
        {
            Instantiate(hotdogBasePrefab, new Vector2(-1, -1), Quaternion.identity); 
            baseSpawned = true; 
        }
    }
}
