using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    public List<GameObject> PetStuff= new List<GameObject>();
    public List<Button> Buttons = new List<Button>();

    public GameObject MinigameThree;
    public GameObject MinigameOne;
    public GameObject beginning;

    private int correctButtonIndex;


    //asked chat about the for each part and index 
    public void Start()
    {
        PickRandomNeed();


    }

    public void OnEnable()
    {

        PickRandomNeed();
    }

    public void PickRandomNeed()
    {
        foreach (GameObject obj in PetStuff)
        {
            obj.SetActive(false);
        }

        correctButtonIndex = Random.Range(0, PetStuff.Count);


        PetStuff[correctButtonIndex].SetActive(true);

    }

    public void OnButtonPress(int buttonIndex)
    {
        if (buttonIndex == correctButtonIndex)
        {
            Debug.Log("Right Button");

            MinigameThree.SetActive(false);
            MinigameOne.SetActive(true);

        }

        else
        {
            Debug.Log("Not button");
            MinigameThree.SetActive(false);
            beginning.SetActive(true);

        }
    }
}