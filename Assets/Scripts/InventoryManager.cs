using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{    
    public TextMeshProUGUI cupsInventoryText;
    public TextMeshProUGUI lemonInventoryText;
    public TextMeshProUGUI sugarInventoryText;
    public TextMeshProUGUI waterInventoryText;
    public TextMeshProUGUI playerMoneyText;

    public GameObject soldOut;

    private int cups = 0;
    private int lemon = 0;
    private int sugar = 0;
    private int water = 0;
    private float playerMoney = 20.00f;

    private float servings = 0f;
    public RecipeManager recipeManager;
    public PriceManager priceManager;

    private enum ItemType { Cup, Lemon, Sugar, Water }

    void Start()
    {
        soldOut.SetActive(true);
    }

    private void UpdateUI()
    {
        cupsInventoryText.text = "Cups: " + cups.ToString();
        lemonInventoryText.text = "Lemon Juice: " + lemon.ToString();
        sugarInventoryText.text = "Sugar: " + sugar.ToString();
        waterInventoryText.text = "Water: " + water.ToString();
        playerMoneyText.text = "$" + playerMoney.ToString("F2");
    }

    private bool PurchaseItem(ItemType itemType, int amount, float costPerUnit)
    {
        float totalCost = costPerUnit;

        if (playerMoney >= totalCost)
        {
            switch (itemType)
            {
                case ItemType.Cup:
                    cups += amount;
                    break;
                case ItemType.Lemon:
                    lemon += amount;
                    break;
                case ItemType.Sugar:
                    sugar += amount;
                    break;
                case ItemType.Water:
                    water += amount;
                    break;
            }
            playerMoney -= totalCost;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("not enough money to purchase " + amount + " " + itemType);
            return false;
        }
    }

    public void PurchaseCups(int amount)
    {
        float costPerCup = amount switch
        {
            10 => 2.50f,
            25 => 3.75f,
            50 => 5.00f,
            _ => 0
        };
        PurchaseItem(ItemType.Cup, amount, costPerCup);
    }

    public void PurchaseLemons(int amount)
    {
        float costPerLemon = amount switch
        {
            10 => 5.00f,
            25 => 10.00f,
            50 => 15.00f,
            _ => 0
        };
        PurchaseItem(ItemType.Lemon, amount, costPerLemon);
    }

    public void PurchaseSugar(int amount)
    {
        float costPerSugar = amount switch
        {
            10 => 2.50f,
            25 => 5.00f,
            50 => 7.50f,
            _ => 0
        };
        PurchaseItem(ItemType.Sugar, amount, costPerSugar);
    }

    public void PurchaseWater(int amount)
    {
        float costPerWater = amount switch
        {
            10 => 2.00f,
            25 => 3.75f,
            50 => 5.00f,
            _ => 0
        };
        PurchaseItem(ItemType.Water, amount, costPerWater);
    }

    public void CalculateServings()
    {
        float lemonPerServing = recipeManager.GetLemonRecipe();
        float sugarPerServing = recipeManager.GetSugarRecipe();
        float waterPerServing = recipeManager.GetWaterRecipe();

        float possibleLemonServings = lemon / lemonPerServing;
        float possibleSugarServings = sugar / sugarPerServing;
        float possibleWaterServings = water / waterPerServing;

        servings = Mathf.Min(possibleLemonServings, possibleSugarServings, possibleWaterServings, cups);
        Debug.Log("Calculated servings: " + servings);

        UpdateUI();
    }

    public bool BuyLemonade()
    {
        if (servings > 0)
        {
            servings--;
            cups--;
            float lemonPerServing = recipeManager.GetLemonRecipe();
            float sugarPerServing = recipeManager.GetSugarRecipe();
            float waterPerServing = recipeManager.GetWaterRecipe();

            lemon -= Mathf.CeilToInt(lemonPerServing);
            sugar -= Mathf.CeilToInt(sugarPerServing);
            water -= Mathf.CeilToInt(waterPerServing);

            float lemonadePrice = priceManager.priceSlider.value;
            playerMoney += lemonadePrice;

            soldOut.SetActive(false);

            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("No more lemonade available to sell.");
            return false;
            soldOut.SetActive(true);
        }
    }


}
