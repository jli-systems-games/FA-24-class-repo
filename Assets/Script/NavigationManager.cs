using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    public Transform spot,plyr;
    [SerializeField] TrailRenderer bullet;
    [SerializeField] Transform gunPoint;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] Rigidbody rb; 
    NavMeshAgent _agent;
    
    TrailRenderer trail;
    bool reached,startedMove,bulletActive;
    public bool gotKilled;
    void Start()
    {   
       
           _agent = GetComponent<NavMeshAgent>();
         
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_agent.remainingDistance);
        float remains = _agent.remainingDistance;
        if(remains == 0 && startedMove)
        {
            reached = true;
            if (reached)
            {
                //Coroutine
                StartCoroutine(findingPlyr());
                reached = false;
            }
        }
    }

    public void Movement()
    {   
     
        _agent.destination = spot.position;
        startedMove = true;

    }

    IEnumerator findingPlyr()
    {
        float remainingDistance;
        Vector3 hitPoint;
        float dist;
      
        while (!this.gotKilled)
        {
            Vector3 direct = plyr.position - gunPoint.position;
            Debug.DrawRay(gunPoint.position, direct, Color.red);
            //Debug.DrawRay(gunPoint.position, gunPoint.forward, Color.green);
            transform.LookAt(plyr.position);
            Ray _ray = new Ray(gunPoint.position, direct);

            yield return new WaitForSeconds(5f);

             //Debug.Log(bulletActive);
            if (!bulletActive)
            {
               
                if(Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
                {
                      
                     trail = Instantiate(bullet, gunPoint.position, Quaternion.identity);
                    Rigidbody cloneRb = trail.GetComponent<Rigidbody>();
                    Vector3 startPos = trail.transform.position;
                    remainingDistance = Vector3.Distance(trail.transform.position, hit.transform.position);
                    hitPoint = hit.point;
                    dist = remainingDistance;
                    bulletActive = true;
                  
                    yield return new WaitForSeconds(2f);
                    cloneRb.isKinematic = false;
                    cloneRb.AddForce(direct * 5f, ForceMode.VelocityChange);
                    //Debug.Log(rb.velocity);
                    Debug.Log(cloneRb.velocity);
                    while (remainingDistance > 0f)
                    {
                        trail.transform.position = Vector3.Lerp(startPos, hitPoint, 1 - (remainingDistance / dist));
                        remainingDistance -= bulletSpeed * Time.deltaTime;
                        yield return null;
                    }
                    Destroy(trail.gameObject);
                    yield return new WaitForSeconds(5f);
                    bulletActive = false;
                  
                }

                

            }


           
            yield return null;
        }
    }

    
}
