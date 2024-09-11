using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2 : MonoBehaviour
{
    public GameObject[] shapePrefabs;   // Array of shape prefabs (assign in Inspector)
    public RectTransform canvasRect;    // Reference to the Canvas RectTransform

    private GameObject[] currentShapes;  // Array to hold the instantiated shapes
    private GameObject correctShape;     // The shape the player needs to click


    // Start is called before the first frame update
    void Start()
    {
        SpawnShapes();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the player clicked on the shape
            Vector2 mousePos = Input.mousePosition;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedShape = hit.collider.gameObject;

                // Check if the clicked shape is the correct one
                if (clickedShape == correctShape)
                {
                    Debug.Log("Correct shape clicked!");
                    DestroyShapes();  // Remove all shapes
                    SpawnShapes(); // Spawn new shapes
                }
                else
                {
                    Debug.Log("Wrong shape! Try again.");
                }
            }
        }
    }

    void SpawnShapes()
    {
        // Initialize the current shapes array
        currentShapes = new GameObject[shapePrefabs.Length];

        // Randomly choose which shape will be the "correct" one
        int correctShapeIndex = Random.Range(0, shapePrefabs.Length);
        correctShape = null;

        // Loop through the shapes and instantiate each one
        for (int i = 0; i < shapePrefabs.Length; i++)
        {
            GameObject shapePrefab = shapePrefabs[i];

            // Instantiate the shape on the canvas
            currentShapes[i] = Instantiate(shapePrefab, canvasRect);

            // Randomize the position of the shape on the screen
            Vector2 randomPosition = new Vector2(
                Random.Range(0, canvasRect.rect.width),
                Random.Range(0, canvasRect.rect.height)
            );
            currentShapes[i].GetComponent<RectTransform>().anchoredPosition = randomPosition;

            // Set the correct shape
            if (i == correctShapeIndex)
            {
                correctShape = currentShapes[i];
            }
        }
    }

    void DestroyShapes()
    {
        // Destroy all current shapes
        for (int i = 0; i < currentShapes.Length; i++)
        {
            Destroy(currentShapes[i]);
        }
    }
}

