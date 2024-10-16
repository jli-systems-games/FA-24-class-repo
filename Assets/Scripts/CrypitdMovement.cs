using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrypitdMovement : MonoBehaviour
{
    NavMeshAgent _agent;
    Vector3 currentPos;
    [SerializeField] EnemyStates _enemyStates;
    [SerializeField] Transform _statsCanvas;
    [SerializeField] Transform plyr;
    
    public List <Transform> moveLocations;
    bool reached;
    float startingValue =  5;
    float maxStat = 10;
    int LastChickCount = 0;
    private void Awake()
    {
        
    }
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        currentPos = moveLocations[Random.Range(0, moveLocations.Count)].position;
        _agent.destination = currentPos;
        eventManager.goFetch += Fetching;
        eventManager.decreaseBoredom += calBoredom;
        eventManager.manageHunger += calHunger;
        eventManager.chickenChecks += calIrrtation;
        eventManager.triggerAttack += Attacking;
        eventManager.resetAttack += Reseting;
        StartCoroutine(StartingStats());
        
    }
    private void OnEnable()
    {
         
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
        }else if(_enemyStates.currentState == CryptidState.Attacking)
        {
            transform.LookAt(plyr);
        }   
        
         
    }
    IEnumerator StartingStats()
    {  
        yield return null;

        foreach(Transform statsBar in _statsCanvas)
        {    Debug.Log(startingValue);
            StatsManager _statM = statsBar.GetComponent<StatsManager>();
            if (_statM != null)
            {
               
                _statM.UpdateStats(startingValue, maxStat);
            }
        }
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

        if(_enemyStates.currentState != CryptidState.Tutorial)
        {   
            if(_enemyStates.currentState != CryptidState.Fetching)
            {
                _enemyStates.ChangeCryState(CryptidState.Fetching);
            
            }

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
            startingValue = startingValue + (startingValue * 0.2f);
            bStats.UpdateStats(startingValue, maxStat);
        }
        else if(sender == "decrease")
        {
            startingValue = startingValue - (startingValue * 0.15f);
            bStats.UpdateStats(startingValue, maxStat);
        }
    }

    void calHunger(GameObject hBar, string sender)
    {
        StatsManager hStats = hBar.GetComponent<StatsManager>();
        if(sender == "increase")
        {
            startingValue = startingValue + (maxStat * 0.2f);
            hStats.UpdateStats(startingValue, maxStat);
        }else if (sender == "decrease")
        {
            startingValue = startingValue - (maxStat * 0.3f);
            hStats.UpdateStats(startingValue, maxStat);
        }
    }
    void calIrrtation(GameObject h, GameObject irrt, string sender)
    {
        StatsManager iStat = irrt.GetComponent<StatsManager>();
        //atsManager hstat = h.GetComponent<StatsManager>();


        if (sender == "increase")
        {
            // increase the irritation meter
            Debug.Log("calc stats");
            startingValue = startingValue + (maxStat * 0.3f);
            iStat.UpdateStats(startingValue, maxStat);
            calHunger(h, sender);

        }else if(sender == "decrease")
        {
             
                 //decrease the meter slightly;
                Debug.Log("lessen irritations");
                startingValue = startingValue - (startingValue * 0.15f);
                Debug.Log("decreased" + startingValue);
                iStat.UpdateStats(startingValue, maxStat);
            
        }
         
        
    }
    void Attacking(GameObject trigger)
    {
        Debug.Log("this is " + trigger.name);
        if(trigger.name == "hunger" || trigger.name == "irritation")
        {
            _enemyStates.ChangeCryState(CryptidState.Attacking);
            
            //play crying baby sound;

        }
    }
    void Reseting()
    {
        Debug.Log("you broke out of the hold");
        _enemyStates.ChangeCryState(CryptidState.Roaming);
            
    }
   
}
