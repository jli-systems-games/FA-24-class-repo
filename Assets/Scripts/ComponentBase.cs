using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComponentBase : MonoBehaviour
{
    public Components _comp;

    public virtual int EnergyOutput(int currentEn, int Max)
    {   int output = currentEn;
        if(currentEn >= -(_comp.energy))
        {
            output = Mathf.Clamp(currentEn + _comp.energy, 0, Max);
        }
         
        
        return output;
    }
}
