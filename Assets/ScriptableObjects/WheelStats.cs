using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this line adds a custom menu object when we right click in the project menu
[CreateAssetMenu(menuName = "Custom/WheelStats")]

public class WheelStats  : ScriptableObject
{
    public float TopSpeed;
    public float Acceleration;
    public float Weight;
    public float Drifting;
    public GameObject WheelPrefab;
    public string WheelName; //name that shows up in UI when wheel is selected

}
