using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    public List<GameObject> colorObjects = new List<GameObject>();
    public List<Button> Buttons = new List<Button>();

    public GameObject MinigameTwo;
    public GameObject MinigameThree;
    public GameObject beginning;
    
    private int correctButtonIndex;



    public void Start()
    {
        PickRandomColor();

    }

    public void PickRandomColor()
    {
        foreach (GameObject obj in colorObjects)
        {
            obj.SetActive(false);
        }

        correctButtonIndex = Random.Range(0, colorObjects.Count);


        colorObjects[correctButtonIndex].SetActive(true);

    }

    public void OnButtonPress(int buttonIndex)
    {
        if (buttonIndex == correctButtonIndex)
        {
            Debug.Log("Good Button");

            MinigameTwo.SetActive(false);
            MinigameThree.SetActive(true);

        }

        else
        {
            Debug.Log("Bad button");
            MinigameTwo.SetActive(false);
            beginning.SetActive(true);

        }
    }
}

