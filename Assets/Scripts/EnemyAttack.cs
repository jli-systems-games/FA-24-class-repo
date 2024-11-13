using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
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

        steps = speed * Time.deltaTime;
        
        transform.position = Vector2.MoveTowards(transform.position, targets[index].position, steps);
        
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.TryGetComponent<ObstacleBase>(out ObstacleBase obs)) 
        {
            
                
            if (collision.collider.CompareTag("blocks"))
            {
                string _id = obs.id;
                EventManager.harming(_id, _stats.damage);
                deductHealth(1);
            }else if (collision.collider.CompareTag("deflecting"))
            {
                int damage = obs._stats.Deflect;
                string _id = obs.id;
                deductHealth(damage);
                EventManager.harming(_id, _stats.damage);

            } 
                
                
         };
      

      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //List<string> targets = new List<string>();
        
        if (other.CompareTag("villager"))
        {
            EventManager.harming(targetIDs[index], _stats.damage);

        }
       
        if(GameManager.enemies.Contains(gameObject)) GameManager.enemies.Remove(gameObject);

        if (GameManager.enemies.Count <= 0)
        {   
            if(_targets.Count > 0)
            {
                EventManager.ChangeState(LevelState.End);
            }
            
        }else if(GameManager.enemies.Count > 0 && _targets.Count <= 0)
        {
            EventManager.ChangeState(LevelState.Defeat);
        }

        deductHealth(1);

           
        
    }
    void deductHealth(int i)
    {
        health -= i;
        if (health <= 0) gameObject.SetActive(false);
    }
    private void ChangingTarget()
    {
        if (index != targets.Length-1) index++;
        else return;
        

    }
}
