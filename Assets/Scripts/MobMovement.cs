using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    IEnumerator movement;

    public Transform plyr, rightSide, leftSide;
    List<Transform> spawnPoints = new List<Transform>();
    public Rigidbody rb;

    float steps;
    public float speed = 2f;
    RaycastHit hit;
    Ray _ray;
    public bool isHunting;
    int pos;
    Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints.Add(rightSide);
        spawnPoints.Add(leftSide);
        movement = Hunt();
        animate = GetComponent<Animator>();

        StartCoroutine(movement);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        steps = speed * Time.deltaTime;
       // _ray = new Ray(transform.position, transform.right);

        if(pos == 0)
        {
            _ray = new Ray(transform.position, -transform.right);
        }
        else
        {
            _ray = new Ray(transform.position, transform.right);
        }
        if(isHunting)
        {
            transform.position = Vector3.MoveTowards(transform.position, plyr.position, steps);

            //isHunting = false;
        }
        else
        {
            animate.SetTrigger("Caught");
        }
        

        if(Physics.Raycast(_ray, out hit, 1f))
        {
            Debug.Log("Jumpscare!");
            isHunting = false;
        }
    }

    private IEnumerator Hunt()
    {
        while (true)
        {
            reSpawn();
            yield return new WaitForSeconds(7f);
        }
        

    }
    
    void reSpawn()
    {
        pos = Mathf.FloorToInt(Random.Range(0, spawnPoints.Count));
        transform.position = spawnPoints[pos].position;
        Debug.Log(pos);
        isHunting = true;


    }
}
