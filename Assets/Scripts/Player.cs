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

    public bool falling;

    public bool canFall;

    public Animator animator;

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
        falling = false;
        //StartCoroutine(Jump());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && falling == false)
        {
                _rb.velocity = transform.up * jumpSpeed;
            numJumps++;

            if (canFall == false && gameManager.flyingTransition == false)
            {
                StartCoroutine(gameManager.Flying());
            }
            else
            {

            }
            if (numJumps > 5)
            {
                _rb.velocity = Vector3.zero;
                gameManager.StopFlying();
            }
            Debug.Log("flying: " + flying);
        }

        //if(_rb.velocity.y < 0)
        //{
        //    falling = true;
        //}
        //else
        //{
        //    falling = false;   
        //}

        if(_rb.velocity.y == 0 && canFall == true)
        {
            flying = false;
            gameManager.StopFlying();
            canFall = false;
            numJumps = 0;
        }
        Debug.Log(numJumps);

    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * speed, Space.World);
            //_rb.velocity = transform.right * speed;
            Vector3 rotationVector = new Vector3(0, 90, 0);
            Quaternion rotation = Quaternion.Euler(rotationVector);
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            animator.Play("walking");
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed, Space.World);
            //_rb.velocity = transform.right * -speed;
            Vector3 rotationVector = new Vector3(0, 270, 0);
            Quaternion rotation = Quaternion.Euler(rotationVector);
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            animator.Play("walking");
        }

        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            animator.Play("still");
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

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Collectable_Item>().eIndicator.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //updateInventory(other.gameObject.GetComponent<Collectable_Item>().item);
            if(other.gameObject.GetComponent<Collectable_Item>().item == Inventory.Equipment)
            {
                addStrength();
            }
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Collectable_Item>().eIndicator.SetActive(false);
    }

    private void addStrength()
    {
        gameManager.updateStats(Inventory.Equipment);
    }
}
