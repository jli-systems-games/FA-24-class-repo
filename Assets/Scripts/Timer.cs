using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    public float timer;

    public Slider timeMeter;

    
    // Start is called before the first frame update
    void Start()
    {
        timeMeter.maxValue = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timeMeter.value = timer;
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 0;
        }

    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void unpauseGame()
    {
        Time.timeScale = 1;
    }
}
