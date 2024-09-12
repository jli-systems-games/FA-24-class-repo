using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public GameObject screen;

    public float C;
    public float S;

    public byte RR;
    public float R;
    public byte GG;
    public float G;
    public byte BB;
    public float B;

    private Coroutine ngCoroutine;
    public float nextGame;

    public void Start()
    {
        InvokeRepeating("ColorChange", 0.1f, 0.1f);

    }

    public void NextGame()
    {
        ngCoroutine = StartCoroutine(PickGame());
    }

    private IEnumerator PickGame()
    {
        nextGame = Random.Range(0, 6);

        if (nextGame == 0)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else if(nextGame == 1)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (nextGame == 2)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (nextGame == 3)
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        else if (nextGame == 4)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
        else if (nextGame == 5)
        {
            SceneManager.LoadScene(5, LoadSceneMode.Single);
        }
    }

    public void ColorChange()
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
