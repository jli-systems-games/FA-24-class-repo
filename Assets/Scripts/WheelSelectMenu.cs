using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
public class WheelSelectMenu : MonoBehaviour
{
    public TextMeshProUGUI CurrentSelectedWheelText;
    public List<WheelStats> WheelOptions;

    private int WheelOptionsIndex = 0;

    public event Action<WheelStats> UpdateWheelType;
    // Start is called before the first frame update
    void Start()
    {
        if(WheelOptions.Count > 0)
        {
            //update the ui object's text according to our currently selected wheel's name
            CurrentSelectedWheelText.text = WheelOptions[WheelOptionsIndex].WheelName;

            //send event to update the player object
            UpdateWheelType?.Invoke(WheelOptions[WheelOptionsIndex]);
        }
    }

    public void NextWheelChoice()
    {
        //if our current option is the last one in our list, go back to the beginning!
        if(WheelOptionsIndex == WheelOptions.Count - 1)
        {
            WheelOptionsIndex = 0;
        }
        else
        {
            WheelOptionsIndex++;
        }

        //same thing we did in the start function -- update your UI text and send the event to the player
        
        //(if the UI were fancier and had its own sprite / mesh + text,
        //we could make our own script for that and make it listen to the event too!)
        CurrentSelectedWheelText.text = WheelOptions[WheelOptionsIndex].WheelName;
        UpdateWheelType?.Invoke(WheelOptions[WheelOptionsIndex]);
    }

    public void PreviousWheelChoice()
    {
        //if our current option is the first one in our list, go to the end!
        if (WheelOptionsIndex == 0)
        {
            WheelOptionsIndex = WheelOptions.Count - 1;
        }
        else
        {
            WheelOptionsIndex--;
        }

        //same thing we did in the start function -- update your UI text and send the event to the player

        //(if the UI were fancier and had its own sprite / mesh + text,
        //we could make our own script for that and make it listen to the event too!)
        CurrentSelectedWheelText.text = WheelOptions[WheelOptionsIndex].WheelName;
        UpdateWheelType?.Invoke(WheelOptions[WheelOptionsIndex]);
    }
}
