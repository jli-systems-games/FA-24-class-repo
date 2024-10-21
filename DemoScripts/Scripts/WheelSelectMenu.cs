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
            CurrentSelectedWheelText.text = WheelOptions[WheelOptionsIndex].WheelName;
            UpdateWheelType?.Invoke(WheelOptions[WheelOptionsIndex]);
        }
    }

    public void NextWheelChoice()
    {
        if(WheelOptionsIndex == WheelOptions.Count - 1)
        {
            WheelOptionsIndex = 0;
        }
        else
        {
            WheelOptionsIndex++;
        }

        CurrentSelectedWheelText.text = WheelOptions[WheelOptionsIndex].WheelName;
        UpdateWheelType?.Invoke(WheelOptions[WheelOptionsIndex]);
    }

    public void PreviousWheelChoice()
    {
        if(WheelOptionsIndex == 0)
        {
            WheelOptionsIndex = WheelOptions.Count - 1;
        }
        else
        {
            WheelOptionsIndex--;
        }
        CurrentSelectedWheelText.text = WheelOptions[WheelOptionsIndex].WheelName;
        UpdateWheelType?.Invoke(WheelOptions[WheelOptionsIndex]);
    }

}
