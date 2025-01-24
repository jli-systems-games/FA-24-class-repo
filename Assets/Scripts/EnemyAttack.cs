using System.Collections;
using System;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float speed = 2f;
    public EnemyInfo _stats;
    [SerializeField] Transform[] targets;
    List<string> targetIDs = new List<string>();
    List<string> _targets = new List<string>();
    
    float steps;
    int index = 0;
    protected int health;
    protected virtual void Start()
    {   
        VillagerBehavior[] villagers = FindObjectsByType<VillagerBehavior>(FindObjectsSortMode.None);
        foreach(var villager in villagers)
        {
            targetIDs.Add(villager.id);
        }

       targets = Array.ConvertAll<VillagerBehavior,Transform>(villagers, (item) => item.transform);
        EventManager.changeTarget += ChangingTarget;
        _targets = villagers[0]._stats.ids;
        health = _stats.Health;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //during start State;
        if(GameManager.currState == LevelState.Start)
        {
          steps = speed * Time.deltaTime;
        
          transform.position = Vector2.MoveTowards(transform.position, targets[index].position, steps);
        }
      
        
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.TryGetComponent<ObstacleBase>(out ObstacleBase obs)) 
        {
            
                
            if (collision.collider.CompareTag("blocks"))
            {
                obs.DeductHealth( _stats.damage);
                deductHealth(1);
            }else if (collision.collider.CompareTag("deflecting"))
            {   
                
                int damage = obs._stats.Deflect;
                
                deductHealth(damage);
                obs.DeductHealth(_stats.damage);

            } 
                
                
         };
      

      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //List<string> targets = new List<string>();
        
        if (other.CompareTag("villager"))
        {
           
            VillagerBehavior v = other.GetComponent<VillagerBehavior>();
            v.DeductHealth(targetIDs[index], _stats.damage);
            //EventManager.harming(targetIDs[index], _stats.damage);

        }

        determine();



    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("villager"))
        {

            VillagerBehavior v = collision.GetComponent<VillagerBehavior>();
            v.DeductHealth(targetIDs[index], _stats.damage);
            //EventManager.harming(targetIDs[index], _stats.damage);

        }

        determine();
    }
    void deductHealth(int i)
    {
        health -= i;
        if (health <= 0)
        {   
            if(GameManager.enemies.Contains(gameObject)) GameManager.enemies.Remove(gameObject);
            
            gameObject.SetActive(false);
        }
        determine();
            
    }
    private void ChangingTarget()
    {
        if (index != targets.Length-1) index++;
        else return;
        

    }
    void determine()
    {

        if (GameManager.enemies.Count <= 0)
        {
            if (_targets.Count > 0)
            {
                EventManager.ChangeState(LevelState.End);
            }

        }
        else if (GameManager.enemies.Count > 0 && _targets.Count <= 0)
        {
            EventManager.ChangeState(LevelState.Defeat);
        }
    }

    private void OnDisable()
    {
        EventManager.changeTarget -= ChangingTarget;

    }
}
