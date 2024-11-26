using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public int minSortingOrder = 3;       // Minimum sorting order
    public int maxSortingOrder = 100;     // Maximum sorting order
    public float sortingMultiplier = -100f; // Multiplier to scale Y position to sorting order

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calculate the sorting order based on the Y position
        int sortingOrder = Mathf.RoundToInt(transform.position.y * sortingMultiplier);

        // Clamp the sorting order between minSortingOrder and maxSortingOrder
        spriteRenderer.sortingOrder = Mathf.Clamp(sortingOrder, minSortingOrder, maxSortingOrder);
    }
}
