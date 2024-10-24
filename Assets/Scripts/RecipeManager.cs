using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public TextMeshProUGUI lemonRecipeText;
    public TextMeshProUGUI sugarRecipeText;
    public TextMeshProUGUI waterRecipeText;

    private float lemonRecipe = 0f;
    private float sugarRecipe = 0f;
    private float waterRecipe = 0f;
    private float increment = 0.2f;

    public void IncreaseLemon()
    {
        if (lemonRecipe < 10.0f)
        {
            lemonRecipe += increment;
            lemonRecipe = Mathf.Min(lemonRecipe, 10.0f);
            UpdateRecipeUI();
        }
    }

    public void DecreaseLemon()
    {
        lemonRecipe = Mathf.Max(0, lemonRecipe - increment);
        UpdateRecipeUI();
    }

    public void IncreaseSugar()
    {
        if (sugarRecipe < 10.0f)
        {
            sugarRecipe += increment;
            sugarRecipe = Mathf.Min(sugarRecipe, 10.0f);
            UpdateRecipeUI();
        }
    }

    public void DecreaseSugar()
    {
        sugarRecipe = Mathf.Max(0, sugarRecipe - increment);
        UpdateRecipeUI();
    }

    public void IncreaseWater()
    {
        if (waterRecipe < 3.0f)
        {
            waterRecipe += increment;
            waterRecipe = Mathf.Min(waterRecipe, 3.0f);
            UpdateRecipeUI();
        }
    }

    public void DecreaseWater()
    {
        waterRecipe = Mathf.Max(0, waterRecipe - increment);
        UpdateRecipeUI();
    }

    private void UpdateRecipeUI()
    {
        lemonRecipeText.text = lemonRecipe.ToString("F1");
        sugarRecipeText.text = sugarRecipe.ToString("F1");
        waterRecipeText.text = waterRecipe.ToString("F1");
    }

    public float GetLemonRecipe()
    {
        return lemonRecipe;
    }

    public float GetSugarRecipe()
    {
        return sugarRecipe;
    }

    public float GetWaterRecipe()
    {
        return waterRecipe;
    }
}
