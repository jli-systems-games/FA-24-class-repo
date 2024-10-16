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

    public GameObject[] slots;
    public bool[] slotsTaken;

    public GameObject foodPrefab;
    public GameObject equipmentPrefab;

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

        for (int i = 0; i < slotsTaken.Length; i++) 
        {
            if (item == Inventory.Food)
            {
                //Instantiate(foodPrefab, slots[i].transform);
            }

            if(item == Inventory.Equipment)
            {
                //Instantiate(foodPrefab,slots[i].transform);
            }
        }
    }
}
