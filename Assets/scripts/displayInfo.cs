using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayInfo : MonoBehaviour
{
    public GameObject iceText;
    public GameObject saltText;
    public GameObject waterText;

    public void iceToggle()
    {
        if(iceText.gameObject.activeSelf == false)
            {
                iceText.gameObject.SetActive(true);
            }
        else
            {
                iceText.gameObject.SetActive(false);
            }
    }

    public void saltToggle()
    {
        if(saltText.gameObject.activeSelf == false)
            {
                saltText.gameObject.SetActive(true);
            }
        else
            {
                saltText.gameObject.SetActive(false);
            }
    }

    public void waterToggle()
    {
        if(waterText.gameObject.activeSelf == false)
            {
                waterText.gameObject.SetActive(true);
            }
        else
            {
                waterText.gameObject.SetActive(false);
            }
    }

}
