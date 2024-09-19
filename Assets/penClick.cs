using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class penClick : MonoBehaviour
{

    public Image image;

    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;

    public bool penClicked = false;
    public int timesClicked;

    bool checkInput = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (penClicked == false && Input.GetKeyDown(KeyCode.X))
        {
            image.sprite = frame2;
            Debug.Log("click");
            
        }

        if (penClicked == false && image.sprite == frame2 && Input.GetKeyUp(KeyCode.X))
        {
            image.sprite = frame3;
            penClicked = true;
            Debug.Log("clickup");

        }

        
        if (penClicked == true && image.sprite == frame3 && Input.GetKeyDown(KeyCode.X))
        {
            image.sprite = frame2;
            Debug.Log("clickdown");

        }

        if (penClicked == true && image.sprite == frame2 && Input.GetKeyUp(KeyCode.X))
        {
            image.sprite = frame4;
            penClicked = false;

        }
        

        if (Input.GetKeyDown(KeyCode.X))
        {
            timesClicked += 1;
        }
        
    }
}
