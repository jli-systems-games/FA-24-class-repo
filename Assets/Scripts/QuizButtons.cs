using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizButtons : MonoBehaviour
{

    public GameObject effect;
    public GameObject currentQuestion;
    public GameObject nextQuestion;


    public void ActivateFX()
    {
        effect.SetActive(true);
    }

    public void NextQuestion()
    {
        currentQuestion.SetActive(false);
        nextQuestion.SetActive(true);
    }

    public void HideUI()
    {
        currentQuestion.SetActive(false);
    }

    
}
