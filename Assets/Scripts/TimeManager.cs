using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    float timePassed = 0, lastCheckTimeBoredom = 0;
    public TMP_Text counter;
    [SerializeField] GameObject boredomBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        //Debug.Log(timePassed);
        counter.text = timePassed.ToString();
        if(Mathf.Round(timePassed) % 10  == 0 && timePassed >= 1)
        {
            Debug.Log("adding Hunger");

        }else if(Mathf.FloorToInt(timePassed / 15) > Mathf.FloorToInt(lastCheckTimeBoredom / 15))
        {
            eventManager.decreaseB(boredomBar, "increase");
            lastCheckTimeBoredom = timePassed;
        }
    }
}
