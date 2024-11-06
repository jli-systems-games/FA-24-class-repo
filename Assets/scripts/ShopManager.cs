using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public List<int> itemCosts;
    public List<GameObject> itemPrefabs;
    public GameObject currentItem;

    private AddPoints pineconeManager;
    public TextMeshProUGUI noPineconesText;
    public TextMeshProUGUI placementUIPrompt;

    private bool isPlacingItem = false;

    private void Start()
    {
        pineconeManager = FindObjectOfType<AddPoints>();
        noPineconesText.gameObject.SetActive(false);
        placementUIPrompt.gameObject.SetActive(false);
    }

    public void ItemSelected(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= itemCosts.Count || itemIndex >= itemPrefabs.Count)
        {
            return;
        }

        int selectedItemCost = itemCosts[itemIndex];
        currentItem = itemPrefabs[itemIndex];

        if (pineconeManager.score >= selectedItemCost)
        {
            pineconeManager.score -= selectedItemCost;
            pineconeManager.AddScore();
            StartPlacingItem();
        }
        else
        {
            StartCoroutine(ShowWarning("Not enough pinecones!"));
        }
    }

    void StartPlacingItem()
    {
        isPlacingItem = true;
        placementUIPrompt.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isPlacingItem && Input.GetMouseButtonDown(0))
        {
            PlaceItem();
        }
    }

    void PlaceItem() // raycast logic
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // casts ray and checks hit in scene
        {
            Instantiate(currentItem, hit.point, Quaternion.identity); // current item selected is placed
            isPlacingItem = false; // no longer placing item
            placementUIPrompt.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowWarning("Cannot be placed!"));
        }
    }

    IEnumerator ShowWarning(string message)
    {
        noPineconesText.text = message;
        noPineconesText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        noPineconesText.gameObject.SetActive(false);
    }
}
