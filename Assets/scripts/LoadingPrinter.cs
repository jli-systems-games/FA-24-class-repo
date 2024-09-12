using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingPrinter : MonoBehaviour
{
    int goal;
    int counted = 0;
    public EventManagers manage;
    public TMP_Text score,respond;
    public TMP_Text goalText;


    // Start is called before the first frame update

    float exp = 1;
    private void OnEnable()
    {
        if (manage.firstpass)
        {
            goal = 5;
        }
        else
        {
            int increment = Mathf.FloorToInt(Mathf.Pow(3, exp));
            goal+= increment;
            exp++;
        }
       goalText.text = "Goal: " + goal.ToString();
    }
    void Start()
    {
        
    }
    private void Update()
    {
         if(counted > goal)
          {
                respond.text = ":[ not too much, it will jam it.";
          }
    }

    //being called when clicked 
    public void countingPapers()
    {
        counted++;
        score.text = counted.ToString();
        
        
    }
    bool determineSucess()
    {
        if(counted == goal)
        {
            
            return true;
        }
        else
        {   
            return false;
            //Debug.Log("://");
        }
    }
    private void OnDisable()
    {
        bool sucess = determineSucess();
        manage.checkforFails(sucess);
        counted = 0;
        score.text = "0";
        respond.text = "Hmmmmm";
    }
}
