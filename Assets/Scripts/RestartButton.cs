using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton: MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject start;
    public GameObject game;
    public GameObject customize;
    

    public void SwitchCanvas()
    {
        currentCanvas.SetActive(false);
        game.SetActive(false);
        customize.SetActive(false);
        start.SetActive(true);
    }

}
