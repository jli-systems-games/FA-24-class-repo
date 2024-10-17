using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    Slider statsBar;
    bool filled;
    void Start()
    {
        statsBar = GetComponent<Slider>();
        eventManager.resetAttack += resetSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if(statsBar.value >= 0.9f && !filled )
        {
            //Debug.Log(transform.name + "Reached the value");
            eventManager.startAttack(this.gameObject);

            if(EnemyStates.currentState == CryptidState.Tutorial)
            {
                EnemyStates.currentState = CryptidState.Roaming;

            }
            filled = true;
        }
    }
    public void UpdateStats(float currentValue, float maxValue)
    {
        statsBar.value = currentValue / maxValue;
    }
    void resetSelf()
    {
        filled = false;
       
        UpdateStats(5f, 10f); 
        //Debug.Log(transform.name + statsBar.value);
    }
}
