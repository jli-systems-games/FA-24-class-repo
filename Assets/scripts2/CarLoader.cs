using System.Collections.Generic;
using UnityEngine;

public class CarLoader : MonoBehaviour
{
    public List<GameObject> carList;

    void Start()
    {
       
        foreach (GameObject car in carList)
        {
            car.SetActive(false);
        }

        
        int selectedIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);
        if (selectedIndex >= 0 && selectedIndex < carList.Count)
        {
            carList[selectedIndex].SetActive(true);
        }
    }
}
