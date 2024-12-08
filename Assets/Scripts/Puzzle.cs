using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject[] candles;

    public GameObject[] doorS;

    public GameObject tiles;

    int candlesLit;

    public bool solved;
    // Start is called before the first frame update
    void Start()
    {
        tiles.SetActive(false);
        solved = false;
    }

    public void CheckCandles()
    {
        foreach (GameObject candle in candles)
        {
            if(candle.GetComponent<CheckCandleLit>().lit == true)
            {
                candlesLit++;
            }
        }

        if(candlesLit == candles.Length)
        {
            solved = true;
            StartCoroutine(OpenDoor());
            tiles.SetActive(true);
        }
        else
        {
            candlesLit = 0;
        }
    }

    public IEnumerator OpenDoor()
    {
        foreach (GameObject door in doorS)
        {
            door.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
            door.SetActive(false);
        }
    }
}
