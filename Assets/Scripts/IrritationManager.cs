using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrritationManager : MonoBehaviour
{
    [SerializeField] GameObject irrtateBar;
    StatsManager iBar;
    int LastChickCount;
    void Start()
    {
       //ventManager.chickenChecks += irritationCheck;
        iBar = irrtateBar.GetComponent<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void irritationCheck()
    {
        //find all chickens in the scene
       //List<GameObject> chickens = new List<GameObject>();
       var chickens = FindObjectsOfType<Balls>();
       int chickenCount = chickens.Length;
        Debug.Log("chick:" + chickenCount);
        //check how many are in the scene;
        if(chickenCount >= 4)
        {
            // increase the irritation meter

            
        }else if(LastChickCount < chickenCount)
        {
            //decrease the meter slightly;
        }
        LastChickCount = chickenCount;
    }
}
