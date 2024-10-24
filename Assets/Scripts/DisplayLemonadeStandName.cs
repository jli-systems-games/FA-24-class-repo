using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLemonadeStandName : MonoBehaviour
{
    public TextMeshProUGUI lemonadeStandNameText;

    void Start()
    {
        string standName = PlayerPrefs.GetString("LemonadeStandName", "Citrus Corner");
        Debug.Log("Named: " + standName);
        lemonadeStandNameText.text = standName; 
    }
}
