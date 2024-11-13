using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelectionManager : MonoBehaviour
{
    public List<GameObject> carList;  
    private int selectedIndex = 0;

    void Start()
    {
        
        for (int i = 0; i < carList.Count; i++)
        {
            carList[i].SetActive(false);
        }
        if (carList.Count > 0)
        {
            carList[0].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            SwitchCar();
        }
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            ConfirmSelection();
        }
    }

    public void SwitchCar()
    {
        
        carList[selectedIndex].SetActive(false);

        
        selectedIndex = (selectedIndex + 1) % carList.Count;
        carList[selectedIndex].SetActive(true);
    }

    public void ConfirmSelection()
    {
        
        PlayerPrefs.SetInt("SelectedCarIndex", selectedIndex);
        PlayerPrefs.Save();

        
        SceneManager.LoadScene("Test");
    }
}
