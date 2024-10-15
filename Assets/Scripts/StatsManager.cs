using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    Slider statsBar;

    void Start()
    {
        statsBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateStats(float currentValue, float maxValue)
    {
        statsBar.value = currentValue / maxValue;
    }
}
