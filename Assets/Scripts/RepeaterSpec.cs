using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterSpec : MonoBehaviour
{

    public int EnergyOutput(int currentEn, int Max)
    {
        int n = Max- currentEn;
        //Debug.Log("newEn" + n);
        int outPut = currentEn + n;
        return outPut;
    }
   
}
