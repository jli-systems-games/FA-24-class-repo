using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCMovement : MonoBehaviour
{   
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    Vector3 randomDirection;
    Ray _ray;
    RaycastHit hit;
    bool gotBLocked;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        RandomizeDirection();
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {   
        _ray = new Ray(transform.position, transform.forward);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //StartCoroutine(Movement());
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
        
       /* if (!gotBLocked)
        {
            StartCoroutine(Movement());
        }*/
        
    }
    void PauseMovement()
    {
        
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        StartCoroutine(CheckandTurn());

    }
    IEnumerator CheckandTurn()
    {
       

        transform.rotation = Quaternion.Euler(transform.rotation.x, Mathf.Clamp(transform.rotation.y * 25f,-180, 180), transform.rotation.z);
        yield return new WaitForSeconds(2f);

        if(Physics.Raycast(_ray,out hit, 5f))
        {
            StartCoroutine(CheckandTurn());
        }
        else
        {
            randomDirection = transform.forward;
            gotBLocked = false;
            StartCoroutine(Movement());
        }
    }

}
