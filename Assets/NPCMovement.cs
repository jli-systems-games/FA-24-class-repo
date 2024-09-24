
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCMovement : MonoBehaviour
{   
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    public GameObject[] spawnPoint;
    Vector3 randomDirection;
    Ray _ray;
    RaycastHit hit;
    bool gotBLocked, isRespawning; 
    float stuckedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        RandomizeDirection();
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        _ray = new Ray(transform.position, transform.forward);
        
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
                Debug.Log(Physics.Raycast(_ray, out hit, 2f));
                
                
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
            //Debug.Log("the angle increased: " + Mathf.Clamp(transform.rotation.y + 75f, 0, 360));

            if (!Physics.Raycast(_ray, out hit,10f))
            {
                randomDirection = transform.forward;
                gotBLocked = false;
                StartCoroutine(Movement());
                yield break;

            }
           yield return null;
        }
        

    }
    void PauseMovement()
    {
        
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        

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

}
