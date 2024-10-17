using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    float timePassed = 0, lastCheckTimeBoredom = 0, lastHungerCheck = 0, lastIrritationCheck = 0;
    float countDown = 10f;
    public TMP_Text counter;
    int lastChickenCount;
    IEnumerator timer;
    [SerializeField] GameObject boredomBar, hungerBar, irritaBar;
    [SerializeField] EnemyStates _statesM;
    [SerializeField] GameManager gManage;
    bool routineStarted;
    void Start()
    {
        eventManager.triggerAttack += _countDown;
        eventManager.resetAttack += survived;
        timer = countingDown();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        //Debug.Log(timePassed);
        //counter.text = timePassed.ToString();
        if(EnemyStates.currentState == CryptidState.Roaming)
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
    IEnumerator SwitchStage()
    {
        yield return null;
        
    }
    IEnumerator countingDown()
    {
        //Debug.Log("countin" + countDown);

        while(countDown > 0.1f)
        {

            countDown -= Time.deltaTime;
            //Debug.Log(countDown);
            counter.text = countDown.ToString();

            yield return null;
        }
        //Debug.Log("you lost your mind");
        gManage.ChangeGState(GameState.End);

    }
    void _countDown(GameObject time)
    {
        if(time.name == "hunger" || time.name == "irritation")
        {
            if (!routineStarted)
            {
                StartCoroutine(timer);
               // Debug.Log("yoooo");
                routineStarted = true;
            }
        }
            
      
    }

    void survived()
    {
        if (routineStarted)
        {
            StopCoroutine(timer);
            counter.text = string.Empty;
            countDown = 10f;
            routineStarted = false;
        }
        

    }
}
