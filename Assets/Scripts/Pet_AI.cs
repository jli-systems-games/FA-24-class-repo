using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    bool clicked;

    public GameObject mouseLoc;
    private Vector3 targetPos;
    private Vector2 petToMouseVector;
    private Vector2 directionToMouse;

    private Rigidbody2D rb;

    public float speed;
    public float rotationSpeed;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        stomache = 10;
        criticalHit = 1;
        clicked = false;

        StartCoroutine(incrementHunger());
    }

    // Update is called once per frame
    void Update()
    {
        if (!asleep)
        {
            stomache = 10 - hunger;
            slider.value = stomache;
        }

        if(stomache <= 0)
        {
            asleep = true;
        }

        if (!clicked)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseLoc.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            petToMouseVector = mouseLoc.transform.position - transform.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            targetPos = mouseLoc.transform.position;
            petToMouseVector = targetPos - transform.position;
            speed = speed + 3;
            Invoke("resumeFollow", 2);
        }
        
        directionToMouse = petToMouseVector.normalized;

        if(petToMouseVector.magnitude < .025)
        {
            transform.position = mouseLoc.transform.position;
        }

        //Debug.Log(directionToMouse);
        //Debug.Log(petToMouseVector.magnitude);
    }

    void resumeFollow()
    {
        speed = speed - 3;
        clicked = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("rock"))
        {
            gameManager.hitRock(other.gameObject);
        }
    }

    private IEnumerator incrementHunger()
    {
        yield return new WaitForSeconds(60f);

        hunger++;
        Debug.Log("hunger: " + hunger);
    }
}
