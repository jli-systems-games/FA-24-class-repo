using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CustomerState
{
    Walking,
    Buying,
    Drinking,
    Reviewing,
    Leaving
}

public class CustomerManager : MonoBehaviour
{
    public RecipeManager recipeManager;
    public InventoryManager inventoryManager;

    public GameObject customerPrefab;
    public Transform spawnPoint;
    public Transform endPoint;
    public float moveSpeed = 0.5f;

    public float appeal = 0f;
    public float satisfaction = 0f;
    public TextMeshProUGUI appealText;
    public TextMeshProUGUI satisfactionText;

    public GameObject thoughts;
    private SpriteRenderer thoughtsSpriteRenderer; 

    private bool isDayActive = false;


    public Sprite excessLemonSprite, deficientLemonSprite; 

    public Sprite excessSugarSprite, deficientSugarSprite;

    public Sprite excessWaterSprite, deficientWaterSprite;

    public void StartDay()
    {
        isDayActive = true;
        thoughtsSpriteRenderer = thoughts.GetComponent<SpriteRenderer>();
        spawnPoint.position = new Vector2(-12, -0.5f);
        endPoint.position = new Vector2(12, -0.5f);

        UpdateAppealText();
        UpdateSatisfactionText();

        StartCoroutine(SpawnCustomers());
    }

    public void EndDay()
    {
        isDayActive = false;
        ResetThoughtsSprite();
    }

    private IEnumerator SpawnCustomers()
    {
        while (isDayActive)
        {
            GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(MoveCustomer(newCustomer.transform));

            float randomInterval = Random.Range(2f, 6f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private IEnumerator MoveCustomer(Transform customerTransform)
    {
        CustomerState currentState = CustomerState.Walking;
        float considerationThreshold = Random.Range(1f, 7f);
        bool hasConsidered = false;


        while (customerTransform.position.x < endPoint.position.x)
        {
            customerTransform.position = Vector3.MoveTowards(
                customerTransform.position,
                endPoint.position,
                moveSpeed * Time.deltaTime);

            if (customerTransform.position.x >= considerationThreshold)
            {
                if (appeal >= 0 && Random.value < 0.5f && !hasConsidered)
                {
                    currentState = CustomerState.Buying;
                    hasConsidered = true;
                    yield return StartCoroutine(BuyLemonade(customerTransform));
                    Debug.Log("Buying With Appeal at 0");
                }
                else if (appeal > 15 && Random.value < 0.7f && !hasConsidered)
                {
                    currentState = CustomerState.Buying;
                    hasConsidered = true;
                    yield return StartCoroutine(BuyLemonade(customerTransform));
                    Debug.Log("Buying With Appeal at 15");
                }
                else if (appeal > 50 && Random.value < 0.8f && !hasConsidered)
                {
                    currentState = CustomerState.Buying;
                    hasConsidered = true;
                    yield return StartCoroutine(BuyLemonade(customerTransform));
                    Debug.Log("Buying With Appeal at 50");
                }
                else if (appeal > 100 && Random.value < 0.9f && !hasConsidered)
                {
                    currentState = CustomerState.Buying;
                    hasConsidered = true;
                    yield return StartCoroutine(BuyLemonade(customerTransform));
                    Debug.Log("Buying With Appeal at 100");
                }
                else
                {
                    yield return StartCoroutine(LeavingLemonade(customerTransform));
                    currentState = CustomerState.Leaving;
                    hasConsidered = true;
                    Debug.Log("Leaving");
                }
            }

            yield return null;
        }

        Destroy(customerTransform.gameObject);
    }

    private IEnumerator BuyLemonade(Transform customerTransform)
    {
        CustomerState currentState = CustomerState.Buying;
        yield return new WaitForSeconds(1.5f);

        bool lemonadeBought = inventoryManager.BuyLemonade();

        if (lemonadeBought)
        {
            Debug.Log("Customer bought lemonade.");
            yield return StartCoroutine(DrinkLemonade(customerTransform));
        }
        else
        {
            Debug.Log("Customer couldn't buy lemonade.");
            yield return StartCoroutine(LeavingLemonade(customerTransform));
        }
    }

    private IEnumerator DrinkLemonade(Transform customerTransform)
    {
        CustomerState currentState = CustomerState.Drinking;
        Debug.Log("Drinking");
        yield return new WaitForSeconds(2f);

        EvaluateLemonade();
        yield return StartCoroutine(ReviewLemonade(customerTransform));
    }

    private IEnumerator ReviewLemonade(Transform customerTransform)
    {
        CustomerState currentState = CustomerState.Reviewing;
        Debug.Log("Reviewing");
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator LeavingLemonade(Transform customerTransform)
    {
        CustomerState currentState = CustomerState.Walking;

        while (customerTransform.position.x < endPoint.position.x)
        {
            customerTransform.position = Vector3.MoveTowards(
                customerTransform.position,
                endPoint.position,
                moveSpeed * Time.deltaTime);

            yield return null;
        }

        Destroy(customerTransform.gameObject);
    }

    private void EvaluateLemonade()
    {
        float lemonRecipe = recipeManager.GetLemonRecipe();
        float sugarRecipe = recipeManager.GetSugarRecipe();
        float waterRecipe = recipeManager.GetWaterRecipe();

        float idealLemon = 2f; // 2 tbsp of lemon
        float idealSugar = 2f; // 2 tbsp of sugar
        float idealWater = 1f; // 1 cup of water

        // Calculate deviations from ideal ratios
        float lemonDeviation = Mathf.Abs(lemonRecipe - idealLemon);
        float sugarDeviation = Mathf.Abs(sugarRecipe - idealSugar);
        float waterDeviation = Mathf.Abs(waterRecipe - idealWater);

        ResetThoughtsSprite();

        // Check largest deviation and update the thoughts sprite accordingly
        if (lemonDeviation > sugarDeviation && lemonDeviation > waterDeviation)
        {
            if (lemonRecipe > idealLemon)
                thoughtsSpriteRenderer.sprite = excessLemonSprite; // Too much lemon
            else
                thoughtsSpriteRenderer.sprite = deficientLemonSprite; // Not enough lemon
        }
        else if (sugarDeviation > lemonDeviation && sugarDeviation > waterDeviation)
        {
            if (sugarRecipe > idealSugar)
                thoughtsSpriteRenderer.sprite = excessSugarSprite; // Too much sugar
            else
                thoughtsSpriteRenderer.sprite = deficientSugarSprite; // Not enough sugar
        }
        else if (waterDeviation > lemonDeviation && waterDeviation > sugarDeviation)
        {
            if (waterRecipe > idealWater)
                thoughtsSpriteRenderer.sprite = excessWaterSprite; // Too much water
            else
                thoughtsSpriteRenderer.sprite = deficientWaterSprite; // Not enough water
        }
        else
        {
            thoughtsSpriteRenderer.sprite = null; // Reset thoughts if no major issue
        }

        // Calculate total deviation
        float totalDeviation = lemonDeviation + sugarDeviation + waterDeviation;

        if (totalDeviation < 1f)
        {
            satisfaction += 1f;
        }
        else
        {
            satisfaction -= 1f;
        }

        UpdateAppealText();
        UpdateSatisfactionText();
    }

    private void UpdateAppealText()
    {
        appealText.text = $"Appeal: {Mathf.Clamp(appeal, 0, 100)}";
    }

    private void UpdateSatisfactionText()
    {
        satisfactionText.text = $"Satisfaction: {Mathf.Clamp(satisfaction, 0, 100)}";
    }

    private void ResetThoughtsSprite()
    {
        thoughtsSpriteRenderer.sprite = null;
    }
}