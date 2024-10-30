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
    public int jumpSpeed;

    private Animator _animator;
    private Rigidbody _rb;

    public int numJumps;

    private bool flying;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        slotNum = 0;

        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKey(KeyCode.Space))
        {
            _rb.velocity = transform.up * jumpSpeed;
            numJumps++;
            if(numJumps == 2)
            {
                gameManager.Flying();
            }
        }

    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rb.velocity = transform.right * speed;
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rb.velocity = transform.right * -speed;
        }
    }

    IEnumerator Jump()
    {
        numJumps = 0;

        yield return new WaitForSeconds(3f);

        StartCoroutine(Jump());
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
