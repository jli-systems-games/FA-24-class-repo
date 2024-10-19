using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zEnd : MonoBehaviour
{
    public GameObject dino;
    public GameObject fish;
    public GameObject plant;

    public FloatScriptableObject Happiness;

    void Start()
    {
        Debug.Log(Happiness.value);

        if (Happiness.value == 2)
        {
            dino.SetActive(true);
            fish.SetActive(false);
            plant.SetActive(false);
        }
        else if (Happiness.value == 1)
        {
            dino.SetActive(false);
            fish.SetActive(true);
            plant.SetActive(false);
        }
        else if (Happiness.value == 0)
        {
            dino.SetActive(false);
            fish.SetActive(false);
            plant.SetActive(true);
        }
    }
}