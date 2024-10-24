using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageTrigger : MonoBehaviour
{
    public TriggerNumber number;

    private IntroPage introPage;
    public enum TriggerNumber
    { 
        Number1, Number2, Number3, Number4, Number5, Number6,
    
    }

    private void Start()
    {
        introPage = FindObjectOfType<IntroPage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (number) 
        {
            case (TriggerNumber.Number1):
                introPage.SwitchToStage3();


                break;
            case (TriggerNumber.Number2):
                introPage.SwitchToStage5();
                break;



        }
    }
}
