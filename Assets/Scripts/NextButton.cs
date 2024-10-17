using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject game;
    public GameObject endCanvas;


    public void SwitchCanvas()
    {
        currentCanvas.SetActive(false);
        game.SetActive(true);

        //endCanvas.SetActive(true);
    }


}
