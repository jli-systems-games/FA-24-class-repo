using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameObject item;

    public TextMeshProUGUI displayText;

    public void zToggle()
    {
        if (item.activeSelf == true)
        {
            item.SetActive(false);
            displayText.text = "off";
        }
        else
        {
            item.SetActive(true);
            displayText.text = "on";
        }
    }
}