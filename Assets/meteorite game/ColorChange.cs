using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    //COLOR CHANGING BLOCK
    // the thing thats chancing colors
    public GameObject screen;

    // to choose the colors
    public float C, S;

    // color values
    public byte RR, GG, BB;
    public float R, G, B;

    //void Start()
    //{
    //    InvokeRepeating("ColorChanging", 0.1f, 0.1f);
    //}

    public bool touched;

    public void OnCollisionEnter(Collision collision)
    {

        ColorChanging();
    }

    public void ColorChanging()
    {
        Paint();
        screen.GetComponent<Renderer>().material.color = new Color32(RR, GG, BB, 255);
    }

    private void Paint()
    {
        C = Random.Range(1, 4);

        if (C == 1)
        {
            //no red
            R = 0;

            S = Random.Range(1, 3);
            //green
            //blue

            if (S == 1)
            {
                G = Random.Range(0, 255);
                B = 255;
            }
            else if (S == 2)
            {
                G = 255;
                B = Random.Range(0, 255);
            }
        }
        else if (C == 2)
        {
            //no green
            G = 0;

            S = Random.Range(1, 3);
            //red
            //blue

            if (S == 1)
            {
                R = Random.Range(0, 255);
                B = 255;
            }
            else if (S == 2)
            {
                R = 255;
                B = Random.Range(0, 255);
            }
        }
        else if (C == 3)
        {
            //no blue
            B = 0;

            S = Random.Range(1, 3);
            //red
            //green

            if (S == 1)
            {
                R = Random.Range(0, 255);
                G = 255;
            }
            else if (S == 2)
            {
                R = 255;
                G = Random.Range(0, 255);
            }
        }

        RR = (byte)R;
        GG = (byte)G;
        BB = (byte)B;

    }
}