using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int slotNum;

    public Sprite foodPrefab;
    public Sprite equipmentPrefab;

    private Vector3 playerPos;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        slotNum = 0;

        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerPos += new Vector3(0, speed, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerPos += new Vector3(0, -speed, 0) * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerPos += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerPos += new Vector3(speed, 0, 0) * Time.deltaTime;
        }

        transform.position = playerPos;
    }

    public void updateInventory(Inventory item)
    {
        inventory.Add(item);
        slots[slotNum].gameObject.GetComponent<Image>().enabled = true;
        slots[slotNum].GetComponent<Item_Drag>().itemType = item;

        if (item == Inventory.Food)
        {
            slots[slotNum].gameObject.GetComponent<Image>().sprite = foodPrefab;
        }

        if(item == Inventory.Equipment)
        {
            slots[slotNum].gameObject.GetComponent<Image>().sprite = equipmentPrefab;
        }
        slotNum++;
    }
}
