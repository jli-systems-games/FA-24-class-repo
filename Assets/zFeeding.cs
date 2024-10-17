using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zFeeding : MonoBehaviour
{
    public GameObject SelfObj;
    public List<GameObject> OtherObj = new();

    public void zPlaceObj()
    {
        SelfObj.SetActive(true);
        foreach (GameObject item in OtherObj)
        {
            item.SetActive(false);
        }
    }
}
