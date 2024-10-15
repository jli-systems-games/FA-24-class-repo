using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrypitdMovement : MonoBehaviour
{
    NavMeshAgent _agent;
    Vector3 currentPos;
    [SerializeField] EnemyStates _enemyStates;
    [SerializeField] StatsManager _stats;
    public List <Transform> moveLocations;
    bool reached;
    float startingBor = 5, startingHung, startingIrr = 5;
    float maxStat = 10;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        currentPos = moveLocations[Random.Range(0, moveLocations.Count)].position;
        _agent.destination = currentPos;
        eventManager.goFetch += Fetching;
        eventManager.decreaseBoredom += calBoredom;
        Debug.Log(startingBor);
        _stats.UpdateStats(startingBor, maxStat);
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemyStates.currentState == CryptidState.Roaming)
        {
              float remains = _agent.remainingDistance;
              //Debug.Log(remains);
              if (remains == 0 && !reached)
              {
                     reached = true;
                    PickPos();
          
              }
             //check and decrease speed;
        }    
    }
    IEnumerator movement()
    {  
        Vector3 closest = Vector3.zero;
        List<Transform> comparinglist = moveLocations;
        bool found = false;
       
        List <Transform> farLocations = new List <Transform>();
        yield break;
        
    }
    void PickPos()
    {
        int index = 0 ;
        Vector3 newPos = Vector3.zero;
        
        index = moveLocations.IndexOf(moveLocations[Random.Range(0, moveLocations.Count)]);
        
        newPos = moveLocations[index].position;
        //Debug.Log("neP" + newPos);
        while(newPos == currentPos)
        {
            index = moveLocations.IndexOf(moveLocations[Random.Range(0, moveLocations.Count)]);
            newPos = moveLocations[index].position;
        }
        
       _agent.destination = newPos;
       
        //Debug.Log(target);
        newPos = currentPos;
        reached = false;
       
    }
    void Fetching(Transform ball)
    {
        _agent.destination = ball.position;
        _agent.speed = 5f;
        if(_enemyStates.currentState != CryptidState.Fetching)
        {
            _enemyStates.ChangeCryState(CryptidState.Fetching);
            
        }
        StartCoroutine(Chasing(ball));
    }
    IEnumerator Chasing(Transform target)
    {
        float dist = Vector3.Distance(transform.position, target.position);
        
          while (dist > 2)
           {
                if (target)
                {
                    dist = Vector3.Distance(transform.position, target.position);
                    //Debug.Log("Chasing" + target.position);
                     _agent.destination = target.position;
                    yield return null;
                }
                else
                {
                Debug.Log("break First");
                    eventManager.resetEnemy();
                    yield break;
                }
                
           }
        
      
    }
    public void calBoredom(GameObject bBar, string sender)
    {
        StatsManager bStats = bBar.GetComponent<StatsManager>();
        if(sender == "increase")
        {
            startingBor = startingBor + (startingBor * 0.2f);
            bStats.UpdateStats(startingBor, maxStat);
        }
        else if(sender == "decrease")
        {
            startingBor = startingBor - (startingBor * 0.15f);
            bStats.UpdateStats(startingBor, maxStat);
        }
    }
   
}
