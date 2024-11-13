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

    public int stomach, speed;

    public GameManager _gameManager;

    public PigeonStates pigeonState;

    public Pigeon_Stats_Base pigeonBase;

    private Transform poopPosition;

    private Vector3 targetPos;
    private Vector3 pigeonToTargetVector;
    private Vector3 directionToMouse;

    private float rotationSpeed = .5f;

    private Rigidbody rb;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        pigeonState = PigeonStates.idle;

        rb = GetComponent<Rigidbody>();
        targetPos = new Vector3 (Random.Range(-30, 35), 0, Random.Range(-20, 24));
    }

    private void Awake()
    {
        StartCoroutine(Pooping());
    }

    private void Update()
    {
        pigeonToTargetVector = targetPos - transform.position;
        directionToMouse = pigeonToTargetVector.normalized;

        // have some sort of event that calls a function when Dead to play the dead animation
    }

    private void FixedUpdate()
    {
        SetVelocity();
        RotateTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        Vector2 current = transform.forward;

        transform.up = Vector3.RotateTowards(current, pigeonToTargetVector, 20, rotationSpeed * Time.deltaTime);
    }

    void SetVelocity()
    {
        if (directionToMouse == Vector3.zero)
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
    }

    private IEnumerator Pooping()
    {
        if(pigeonState == PigeonStates.regularBowels)
        {
            poopPosition = gameObject.transform;
            Instantiate(pigeonBase.splatter, poopPosition);
        }
        yield return new WaitForSeconds(5);

        StartCoroutine(Pooping());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Team1Poop") && TeamNum == 2)
        {
            pigeonState = PigeonStates.dead;
        }

        if(other.gameObject.CompareTag("Team2Poop") && TeamNum == 1)
        {
            pigeonState = PigeonStates.dead;
        }
    }
}
