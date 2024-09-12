using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{


    public GameObject start;
    public GameObject minigameOne;


    // Start is called before the first frame update
    public void SwitchCanvas()
    {
        start.SetActive(false);
        minigameOne.SetActive(true);


    }

}
