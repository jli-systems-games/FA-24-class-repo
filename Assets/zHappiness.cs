using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zHappiness : MonoBehaviour
{
    public List<GameObject> SedPref = new();
    public List<GameObject> IgnPref = new();
    public List<GameObject> MetPref = new();

    //public GameObject SedFood;
    //public GameObject IgnFood;
    //public GameObject MetFood;

    //public GameObject IgnWater;
    //public GameObject MetWater;
    //public GameObject SedWater;

    public FloatScriptableObject Happiness;

    void Start()
    {
        Happiness.value = 0f;
    }

    public void zCalculateHappiness()
    {
        if (z_Bools.SedRock == true)
        {
            foreach (GameObject item in SedPref)
            {
                if (item.activeSelf)
                {
                    Happiness.value++;
                }
            }
        }
        else if (z_Bools.IgnRock == true)
        {
            foreach (GameObject item in IgnPref)
            {
                if (item.activeSelf)
                {
                    Happiness.value++;
                }
            }
        }
        else if (z_Bools.MetRock == true)
        {
            foreach (GameObject item in MetPref)
            {
                if (item.activeSelf)
                {
                    Happiness.value++;
                }
            }
        }
    }
}
