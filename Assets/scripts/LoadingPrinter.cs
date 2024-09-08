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
    public TMP_Text score;
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
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("the goal is " + goal);
        //Debug.Log(counted);
    }
    //being called when clicked 
    public void countingPapers()
    {
        counted++;
        score.text = counted.ToString();
        //determineSucess(counted);
        
    }
    bool determineSucess()
    {
        if(counted >= goal)
        {
            
            Debug.Log("Yay");
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
    }
}
