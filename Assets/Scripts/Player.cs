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

    public bool flying;

    public bool canFall;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        slotNum = 0;

        playerPos = transform.position;

        flying = false;
        canFall = false;
        //StartCoroutine(Jump());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
                _rb.velocity = transform.up * jumpSpeed;
            numJumps++;

            if (flying == false)
            {
                StartCoroutine(gameManager.Flying());
            }
            if (numJumps > 5)
            {
                flying = false;
                _rb.velocity = Vector3.zero;
                gameManager.StopFlying();
                numJumps = 0;
            }
            Debug.Log("flying: " + flying);
        }

        if(_rb.velocity.y == 0)
        {
            flying = false;
            numJumps = 0;
        }
        else
        {
            flying = true;
        }
        Debug.Log(numJumps);

    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * speed, Space.World);
            //_rb.velocity = transform.right * speed;
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed, Space.World);
            //_rb.velocity = transform.right * -speed;
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
    }

    public void Fly()
    {
        if (numJumps > 5)
        {
            flying = false;
            _rb.velocity = Vector3.zero;
            gameManager.StopFlying();
            numJumps = 0;
        }
        else
        {
            _rb.velocity = transform.up * jumpSpeed;
        }
    }

    IEnumerator Jump()
    {
        numJumps = 0;

        yield return new WaitForSeconds(.5f);

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
