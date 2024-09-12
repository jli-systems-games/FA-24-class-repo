using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public GameObject popupOne;
    public GameObject popupTwo;
    public GameObject popupThree;

    public GameObject MinigameTwo;
    public GameObject Currentgame;

    public float Score = 0;


    public void Start()
    {
        StartCoroutine(DelayPopUp());

        Score = 0;
    }


    IEnumerator DelayPopUp()
    {

        yield return new WaitForSeconds(1);

        popupOne.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        popupTwo.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        popupThree.gameObject.SetActive(true);
    }


    public void Restart()
    {
        if (Score == 3)
        {
          
            Currentgame.SetActive(false);
            MinigameTwo.SetActive(true);

        }



    }



}
