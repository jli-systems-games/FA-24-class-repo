using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inventory
{
    Food,
    Equipment
}
public class Player : MonoBehaviour
{
    private GameManager gameManager;

    public static List<Inventory> inventory = new List<Inventory>();
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInventory(Inventory item)
    {
        inventory.Add(item);
        foreach (Inventory inventoryItem in inventory)
        {
            Debug.Log(inventoryItem.ToString());
        }
    }
}
