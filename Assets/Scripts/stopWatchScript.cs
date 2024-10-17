using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class stopWatchScript : MonoBehaviour
{
    public float timeStart;
    public TextMeshProUGUI textBox;
    

    void Start()
    {
        textBox.text = timeStart.ToString("F2");
    }


    void update()
    {
        timeStart += Time.deltaTime;
        textBox.text = timeStart.ToString("F2");
    }




}
