using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class refuteProgress : MonoBehaviour
{
    Slider statsBar;
    bool filled;
    void Start()
    {
        statsBar = GetComponent<Slider>();
        eventManager.resetAttack += reserItself;
    }

    // Update is called once per frame
    void Update()
    {
        if (statsBar.value >= 1)
        {
            Debug.Log("reseting");
            eventManager.ResetAttk();
            filled = true;
        }
    }
    public void UpdateProgress(float currentValue, float maxValue)
    {
        statsBar.value = currentValue / maxValue;
    }
    void reserItself()
    {
        statsBar.value = 0;
    }
}
