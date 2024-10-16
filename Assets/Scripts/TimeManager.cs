using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    float timePassed = 0, lastCheckTimeBoredom = 0, lastHungerCheck = 0, lastIrritationCheck = 0;
    public TMP_Text counter;
    int lastChickenCount;
    [SerializeField] GameObject boredomBar, hungerBar, irritaBar;
    [SerializeField] EnemyStates _statesM;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        //Debug.Log(timePassed);
        counter.text = timePassed.ToString();
        if(_statesM.currentState == CryptidState.Roaming)
        {
            if(Mathf.FloorToInt(timePassed / 20) > Mathf.FloorToInt(lastHungerCheck / 20))
            {
            //Debug.Log("adding Hunger");
                eventManager.calcHunger(hungerBar, "increase");
                lastHungerCheck = timePassed;

            }
            else if(Mathf.FloorToInt(timePassed / 25) > Mathf.FloorToInt(lastCheckTimeBoredom / 25))
            {
                eventManager.decreaseB(boredomBar, "increase");
                lastCheckTimeBoredom = timePassed;

            }
            else if(Mathf.FloorToInt(timePassed /15) > Mathf.FloorToInt(lastIrritationCheck / 15))
            {
                //call event that check how many chickens are in the scene.
                var chickens = FindObjectsOfType<Balls>();
                int chickenCount = chickens.Length;
                if(chickenCount >= 4 ) 
                { 
                    eventManager.countChicks(hungerBar, irritaBar, "increase");
                }
                else
                {   
                    if(chickenCount < lastChickenCount)
                    {
                        eventManager.countChicks(hungerBar, irritaBar, "decrease");
                    }
                
                }
                lastChickenCount = chickenCount;
                lastIrritationCheck = timePassed;
            }

        }
        

    }
}
