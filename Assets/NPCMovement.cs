
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Rendering;

public class NPCMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] AudioSource laughter;
    [SerializeField] Transform plyr;
    [SerializeField] Animator anim;
    public GameObject[] spawnPoint;
    Vector3 randomDirection;
    Ray _ray;
    RaycastHit hit;
    bool gotBLocked, isRespawning; 
    float stuckedTime = 0;
    float plyDistance;
    [SerializeField]StartGameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        RandomizeDirection();
        StartCoroutine(Movement());
        StartCoroutine(audioPlay());
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        _ray = new Ray(transform.position, transform.forward);
        plyDistance = Vector3.Distance(transform.position, plyr.position); 
        if(plyDistance > 8f)
        {
            laughter.volume -= Time.deltaTime;
        }else
        {
            laughter.volume += Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            Respawn();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
       
    }

    void RandomizeDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(-1f, 1f)).normalized;
    }

    IEnumerator Movement()
    {
     
        while (!gotBLocked)
        {   
            rb.velocity = randomDirection * moveSpeed;
           if(Physics.Raycast(_ray,out hit, 3f))
            {
                
                gotBLocked = true;
            }
            else
            {
                gotBLocked = false;
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        PauseMovement();
        yield return new WaitForSeconds(1f);
        while (gotBLocked)
        {
            //Debug.Log("angle is " + transform.rotation.y);
            transform.Rotate(Vector3.up * 10f);

            if (!Physics.Raycast(_ray, out hit,10f))
            {
                randomDirection = transform.forward;
                gotBLocked = false;
                StartCoroutine(Movement());
                //walking animation
                anim.SetTrigger("Jump");
                yield break;

            }
           yield return null;
        }
        

    }
    void PauseMovement()
    {
        
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        //trigger idle animation.
        anim.SetTrigger("Idle");
    }
    

    public void Respawn()
    {   
        isRespawning = true;
        int index = Mathf.FloorToInt(Random.Range(0,spawnPoint.Length));
        //Debug.Log(index);
        transform.position = spawnPoint[index].transform.position;
        stuckedTime = 0;
        isRespawning = false;
    }

    IEnumerator audioPlay()
    {
        while (true)
        {   yield return new WaitForSeconds(2f);

          
            laughter.Play();

            yield return new WaitForSeconds(2f);

            laughter.Play();

            yield return new WaitForSeconds(2f);

        }
        

    }

}
