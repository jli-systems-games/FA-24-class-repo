using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zDecorate : MonoBehaviour
{
    public GameObject SedItem;
    public GameObject IgnItem;
    public GameObject MetItem;

    public void zClothe()
    {
        Debug.Log(z_Bools.SedRock);
        Debug.Log(z_Bools.IgnRock);
        Debug.Log(z_Bools.MetRock);
        Debug.Log("click");

        if (z_Bools.SedRock == true)
        {
            if (SedItem.activeSelf)
            {
                SedItem.SetActive(false);
            }
            else
            {
                SedItem.SetActive(true);
            }
        }
        else if (z_Bools.IgnRock == true)
        {

            if (IgnItem.activeSelf)
            {
                IgnItem.SetActive(false);
            }
            else
            {
                IgnItem.SetActive(true);
            }
        }
        else if (z_Bools.MetRock == true)
        {
            if (MetItem.activeSelf)
            {
                MetItem.SetActive(false);
            }
            else
            {
                MetItem.SetActive(true);
            }
        }
    }
}
