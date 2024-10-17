using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private float pets;

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
    public TextMeshProUGUI confidenceMeter;
    public TextMeshProUGUI strengthMeter;


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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseLoc.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        if (gameManager.gameState == GameState.Overworld)
        {
            if (asleep == false)
            {
                if (!clicked)
                {
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

                if (petToMouseVector.magnitude < .025)
                {
                    transform.position = mouseLoc.transform.position;
                }
            }
        }
        else
        {
            transform.position = Vector2.zero;
        }

        //Debug.Log(directionToMouse);
        //Debug.Log(petToMouseVector.magnitude);
    }

    

    #region Movement
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

    #endregion

    #region Damage
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rock"))
        {
            gameManager.hitRock(other.gameObject);
        }
    }

    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManager.gameState == GameState.PetManager)
        {
            if (other.gameObject.CompareTag("mouse"))
            {
                StartCoroutine(increaseConfidence());
            }
        }
    }
    

    #region Petting

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameManager.gameState == GameState.PetManager)
        {
            if (other.gameObject.CompareTag("mouse") && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
            {
                pets = pets + .1f;
                Debug.Log("pets: " + pets);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameManager.gameState == GameState.PetManager)
        {
            if (other.gameObject.CompareTag("mouse"))
            {
                StopCoroutine(increaseConfidence());

                Debug.Log("stopped petting");
            }
        }
    }

    private IEnumerator increaseConfidence()
    {
        yield return new WaitForSeconds(5f);

        if (pets > 5)
        {
            confidence++;
            Debug.Log("confidence: " + confidence);
        }


        pets = 0;
        ConfidenceLevel();

        StartCoroutine(increaseConfidence());
    }
    #endregion

    #region increments
    private IEnumerator incrementHunger()
    {
        yield return new WaitForSeconds(60f);

        hunger++;
        Debug.Log("hunger: " + hunger);

        if (!asleep)
        {
            stomache = 10 - hunger;
            slider.value = stomache;
        }

        if (stomache <= 0)
        {
            asleep = true;
            confidenceMeter.text = "Feeling: Zzz...";
        }
    }

    private IEnumerator incrementConfidence()
    {
        yield return new WaitForSeconds(120f);

        if(pets < 0)
        {
            if(confidence > 0)
            {
                confidence--;
            }
        }

        ConfidenceLevel();
    }
    #endregion

    #region Increase Stats

    public void ConfidenceLevel()
    {
        if (confidence == 1)
        {
            confidenceMeter.text = "Feeling: :|";
        }

        if (confidence == 2)
        {
            confidenceMeter.text = "Feeling: :>";
        }

        if (confidence == 3)
        {
            confidenceMeter.text = "Feeling: :)";
        }

        if (confidence == 4)
        {
            confidenceMeter.text = "Feeling: :]";
        }

        if (confidence == 5)
        {
            confidenceMeter.text = "Feeling: :D";
        }

        if (confidence == 6)
        {
            confidenceMeter.text = "Feeling: ;D";
        }

        if (confidence == 7)
        {
            confidenceMeter.text = "Feeling: :}";
        }

        if (confidence == 8)
        {
            confidenceMeter.text = "Feeling: :3";
        }

        if (confidence == 9)
        {
            confidenceMeter.text = "Feeling: ;3";
        }
        if (confidence == 10)
        {
            confidenceMeter.text = "Feeling: ;33 :DDDDDD !!!!";
        }
    }

    public void AddStrength()
    {
        strength++;

        StrengthLevel();
    }

    void StrengthLevel()
    {
        if(strength == 5)
        {
            strengthMeter.text = "Strength: Weak :(";
        }

        else if(strength == 6)
        {
            strengthMeter.text = "Strength: Getting there!";
        }

        else if (strength == 7)
        {
            strengthMeter.text = "Strength: Moderate";
        }

        else if(strength == 8)
        {
            strengthMeter.text = "Strength: Rather strong";
        }
        else if (strength == 9)
        {
            strengthMeter.text = "Strength: Strong!!";
        }

        else if ( strength == 10)
        {
            strengthMeter.text = "Strength: VERY STRONG!!!";
        }
    }

    public void AddFood()
    {
        hunger--;
        stomache = 10 - hunger;
        slider.value = stomache;

        if (asleep)
        {
            asleep = false;
            ConfidenceLevel();
        }
    }
    #endregion
}
