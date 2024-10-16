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
        if(statsBar.value == 1 && !filled )
        {
            Debug.Log(transform.name + "Reached the value");
            eventManager.startAttack(this.gameObject);
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
    }
}
