using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PigeonStates
{
    idle,
    regularBowels,
    diarrhea,
    dead
}
public class PigeonController : MonoBehaviour
{
    public int TeamNum;

    public int stomachLevel, speed;

    public GameManager _gameManager;

    public PigeonStates pigeonState;

    public Pigeon_Stats_Base pigeonBase;

    private Vector3 poopPosition;

    private Vector3 targetPos;
    private Vector3 pigeonToTargetVector;
    private Vector3 directionToMouse;

    private float rotationSpeed = .5f;

    private Rigidbody rb;

    private bool instantiated;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        pigeonState = PigeonStates.idle;

        rb = GetComponent<Rigidbody>();
        targetPos = new Vector3 (Random.Range(-30, 35), 0, Random.Range(-20, 24));
        instantiated = false;
    }

    private void Awake()
    {
        //StartCoroutine(Pooping());
    }

    private void Update()
    {
        pigeonToTargetVector = targetPos - transform.position;
        directionToMouse = pigeonToTargetVector.normalized;

        if(GameManager.state == GameStates.actingBattle)
        {
            pigeonState = PigeonStates.regularBowels;
        }

        //check whats in the stomach and if there's chocolate, have the the "bomb" go off at a random time within the time limit

        // have some sort of event that calls a function when Dead to play the dead animation
    }

    private void FixedUpdate()
    {
        if (GameManager.state == GameStates.actingBattle && pigeonState != PigeonStates.dead)
        {
            SetVelocity();
            RotateTowardsTarget();
        }
    }

    void RotateTowardsTarget()
    {
        Vector2 current = transform.forward;

        transform.up = Vector3.RotateTowards(current, pigeonToTargetVector, 20, rotationSpeed * Time.deltaTime);
    }

    void SetVelocity()
    {
        //Debug.Log(directionToMouse);
        if (directionToMouse.x < 0.1f && directionToMouse.z < .1f) //directionToMouse.x == 
        {
            rb.velocity = Vector3.zero;
            nextPosition();
        }
        else
        {
            rb.velocity = transform.up * speed;
        }
    }

    void nextPosition()
    {
        targetPos = new Vector3(Random.Range(-30, 35), 0, Random.Range(-20, 24));

        if (instantiated)
        {
            poopPosition = gameObject.transform.position;
            poopPosition.y = 0.01f;

            GameObject paintSplatter = Instantiate(pigeonBase.splatter, poopPosition, Quaternion.identity);
            paintSplatter.transform.Rotate(90, 0, 0);

            if (TeamNum == 1)
            {
                GameManager.teamOneSplatters.Add(paintSplatter);
            }

            else if (TeamNum == 2)
            {
                GameManager.teamTwoSplatters.Add(paintSplatter);
            }
        }
            Debug.Log("Hello");
        instantiated = true;
        //rb.velocity = transform.up * speed;
    }

    private IEnumerator Pooping()
    {
        if(pigeonState == PigeonStates.regularBowels)
        {
            poopPosition = gameObject.transform.position;
            GameObject paintSplatter = Instantiate(pigeonBase.splatter, poopPosition, Quaternion.identity);
            paintSplatter.transform.Rotate(90,0,0);

            Debug.Log("pooping");
        }

        Debug.Log("pigeon state: " + pigeonState);
        yield return new WaitForSeconds(5);

        StartCoroutine(Pooping());
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Team1Poop") && TeamNum == 2)
        {
            pigeonState = PigeonStates.dead;
            //StopCoroutine(Pooping());
        }

        if(other.gameObject.CompareTag("Team2Poop") && TeamNum == 1)
        {
            pigeonState = PigeonStates.dead;
            //StopCoroutine(Pooping());
        }
    }
}
