using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_AI : MonoBehaviour
{
    private GameManager gameManager;

    public int hunger;
    public int strength;
    public int confidence;
    private int stomache;
    public int damage;
    private float criticalChance;
    private int criticalHit;

    public bool asleep;

    public GameObject mouseLoc;
    private Vector2 petToMouseVector;
    private Vector2 directionToMouse;

    private Rigidbody2D rb;

    public float speed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        stomache = 10;
        criticalHit = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!asleep)
        {
            stomache = 10 - hunger;
        }

        if(stomache <= 0)
        {
            asleep = true;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseLoc.transform.position = new Vector3(mousePos.x,mousePos.y,0);
        petToMouseVector = mouseLoc.transform.position - transform.position;
        directionToMouse = petToMouseVector.normalized;

        Debug.Log(directionToMouse);
    }

    private void FixedUpdate()
    {
        SetVelocity();
        RotateTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        Vector2 current = transform.forward;

        transform.up = Vector3.RotateTowards(current, petToMouseVector, 20, rotationSpeed * Time.deltaTime);
    }

    void SetVelocity()
    {
        if(directionToMouse == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }

        else
        {
            rb.velocity = transform.up * speed;
        }
    }

    public void calculateDamage()
    {
        criticalChance = Random.Range(0,100);

        if(criticalChance <= confidence)
        {
            criticalHit = 2;
        }

        else
        {
            criticalHit = 1;
        }

        damage = (strength - hunger) * criticalHit;
    }
}
