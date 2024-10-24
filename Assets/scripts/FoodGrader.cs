using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodGrader : MonoBehaviour
{
    public GameObject foodPanel;
    public TextMeshProUGUI gradeText; 
    private int ingredientCount = 0; 
    private bool hasBadIngredients = false; 
    private string selectedBase = ""; // tracks selected base

    public void SetBase(string baseName)
    {
        selectedBase = baseName;
    }

    public void AddIngredient(bool isBadIngredient)
    {
        ingredientCount++;

        // when bad ingredient added, set the flag to true
        if (isBadIngredient)
        {
            hasBadIngredients = true;
        }
    }

    public void GradeFood()
    {
        string grade;

        if (selectedBase == "Pizza" && hasBadIngredients) // pizza + bad ingredients
        {
            grade = "Grade C: That doesn't belong on pizza!";
            Debug.Log("bad");
        }
        else if (selectedBase == "HotDog" && (hasBadIngredients || ingredientCount > 0)) // hot dog with cheese/lettuce
        {
            grade = "Grade C: Hotdogs shouldn't have these ingredients!";
            Debug.Log("bad");
        }
        else if (ingredientCount > 35 || hasBadIngredients)
        {
            grade = "Grade F: This doesn't taste good :(";
            Debug.Log("bad");
        }
        else if (ingredientCount <= 0)
        {
            grade = "Grade F: Did you even try?";
            Debug.Log("bad");
        }
        else
        {
            grade = "Grade A: So yummy! Om nom nom";
            Debug.Log("good");
        }

        gradeText.text = grade;
        foodPanel.SetActive(true); 
    }

    public void ResetGrading()
    {
        ingredientCount = 0; 
        hasBadIngredients = false;
        selectedBase = ""; 
        gradeText.text = ""; 
        foodPanel.SetActive(false);
    }
}
