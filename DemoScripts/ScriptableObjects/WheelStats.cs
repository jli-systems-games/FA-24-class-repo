using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/WheelStats")]
public class WheelStats : ScriptableObject
{
    public float TopSpeed;
    public float Acceleration;
    public float Weight;
    public float Drifting;
    public GameObject WheelPrefab;
    public string WheelName;
}
