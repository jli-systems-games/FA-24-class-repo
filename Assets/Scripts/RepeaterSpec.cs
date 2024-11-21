using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterSpec : ComponentBase
{

    public override int EnergyOutput(int currentEn)
    {
        int n = _comp.energy - currentEn;
        //Debug.Log("newEn" + n);
        int outPut = currentEn + n;
        return outPut;
    }
   
}
