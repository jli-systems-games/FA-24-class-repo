using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blush : MonoBehaviour
{
    public checkGB eyeShadowStat;

    public Image hanako;
    public Sprite GERFB;
    public Sprite BERFB;
    
    void Start()
    {
        if (eyeShadowStat.eyeshadowGood == true)
        {
            hanako.sprite = GERFB;
        }

        if (eyeShadowStat.eyeshadowGood == false)
        {
            hanako.sprite = BERFB;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
