using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gradiantScript : MonoBehaviour
{

    public float Saturation;
    public float Value;
    public float Speed;

    
    void Update()
    {

        GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(Time.time/Speed, Saturation, Value);


    }
}

