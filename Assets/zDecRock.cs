using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zDecRock : MonoBehaviour
{

    public GameObject SedRockObj;
    public GameObject IgnRockObj;
    public GameObject MetRockObj;


    // Start is called before the first frame update
    void Start()
    {
        if (z_Bools.SedRock == true)
        {
            SedRockObj.SetActive(true);
            IgnRockObj.SetActive(false);
            MetRockObj.SetActive(false);
        }
        else if (z_Bools.IgnRock == true)
        {
            SedRockObj.SetActive(false);
            IgnRockObj.SetActive(true);
            MetRockObj.SetActive(false);
        }
        else if (z_Bools.MetRock == true)
        {
            SedRockObj.SetActive(false);
            IgnRockObj.SetActive(false);
            MetRockObj.SetActive(true);
        }
    }
}
