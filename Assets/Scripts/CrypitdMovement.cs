using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrypitdMovement : MonoBehaviour
{
    NavMeshAgent _agent;
    public Transform[] moveLocations;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
     // _agent.destination = moveLocations[Random.Range(0, moveLocations.Length)].position;
        StartCoroutine(movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator movement()
    {  
        Vector3 closest = Vector3.zero;

        while (true)
        {
            
            float closestDist = Mathf.Infinity;
            for(int i = 0; i < moveLocations.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, moveLocations[i].position);
                //Debug.Log("dist :" + dist + "closest" + closestDist);
                if (dist < closestDist)
                {
                    //Debug.Log("found somthing");
                    closestDist =dist;
                    closest = moveLocations[i].position;
                }
            }

            _agent.destination = closest;
            //Debug.Log(closest);

            yield return null;
        }
        
    }
}
