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
    }

    // Update is called once per frame
    void Update()
    {

        steps = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targets[index].position, steps);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("villager")) EventManager.harming(targetIDs[index]);
       
        gameObject.SetActive(false);
        
    }
    private void ChangingTarget()
    {
        if (index != targets.Length) index++;
        else return;
        

    }
}
