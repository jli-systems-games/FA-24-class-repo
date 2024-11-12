using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] Transform[] targets;
    List<string> targetIDs = new List<string>();
    List<string> _targets = new List<string>();
    float steps;
    int index = 0;
    void Start()
    {   
        VillagerBehavior[] villagers = FindObjectsByType<VillagerBehavior>(FindObjectsSortMode.None);
        foreach(var villager in villagers)
        {
            targetIDs.Add(villager.id);
        }

       targets = Array.ConvertAll<VillagerBehavior,Transform>(villagers, (item) => item.transform);
        EventManager.changeTarget += ChangingTarget;
        _targets = villagers[0]._stats.ids;
    }

    // Update is called once per frame
    void Update()
    {

        steps = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targets[index].position, steps);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //List<string> targets = new List<string>();
        
        if (other.CompareTag("villager"))
        {
            EventManager.harming(targetIDs[index]);
           /* VillagerStats stat = other.GetComponent<VillagerBehavior>()._stats;
            targets = stat.ids;*/

        }
        
        Debug.Log(_targets.Count);
       
        if(GameManager.enemies.Contains(gameObject)) GameManager.enemies.Remove(gameObject);

        if (GameManager.enemies.Count <= 0)
        {   
            if(_targets.Count > 0)
            {
                EventManager.ChangeState(LevelState.End);
            }
          /*  else
            {
                EventManager.ChangeState(LevelState.Defeat);
            }*/
            
        }else if(GameManager.enemies.Count > 0 && _targets.Count <= 0)
        {
            EventManager.ChangeState(LevelState.Defeat);
        }


            gameObject.SetActive(false);
        
    }
    private void ChangingTarget()
    {
        if (index != targets.Length-1) index++;
        else return;
        

    }
}
