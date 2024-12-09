using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Animate : MonoBehaviour
{
    Animator ui;
    Image img;
    bool isdown;
    public GameObject child;
    void Start()
    {
        ui = GetComponent<Animator>();
        img = GetComponent<Image>();
    }

    public void hamburgerPress()
    {
        if (!isdown)
        {
            //start down aniamtion;

            StartCoroutine(drop());
            //img.enabled = true;
            
            isdown = true;
        }
        else
        {
            StartCoroutine (roll());
           
            isdown = false;
        }
    }
    IEnumerator drop()
    {
           if (ui.GetCurrentAnimatorStateInfo(0).IsName("Default"))
            {
                ui.SetTrigger("Start");
            }
            else
            {
                ui.SetBool("Off", false);
            }
           yield return new WaitForSeconds(0.75f);
            child.SetActive(true);
    }
    IEnumerator roll()
    {
        ui.SetBool("Off", true);
        yield return new WaitForSeconds(0.5f);
        child.SetActive(false);
    }
}
