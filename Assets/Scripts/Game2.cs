using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game2 : MonoBehaviour
{
    public GameObject[] sceneShapes;    // Array of shapes already in the scene (assign in Inspector)
    public TextMeshProUGUI randomLetterText; // Reference to the TextMeshPro Text (assign in Inspector)

    private GameObject correctShape;    // The shape the player needs to clickk

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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedShape = hit.collider.gameObject;

                // Check if the clicked shape is the correct one
                if (clickedShape == correctShape)
                {
                    Debug.Log("Correct shape clicked!");
                    SpawnShapes();  // Randomize positions and pick a new target shape
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
        // Randomly choose which shape will be the "correct" one
        int correctShapeIndex = Random.Range(0, sceneShapes.Length);
        correctShape = sceneShapes[correctShapeIndex];

        // Loop through the scene shapes and reposition each one
        foreach (GameObject shape in sceneShapes)
        {
            // Randomize the position of the shape in world space
            shape.transform.position = new Vector3(
                Random.Range(-7f, 7f),  // Adjust these values based on your world boundaries
                Random.Range(-3.5f, 3.5f),  // Adjust these values based on your world boundaries
                shape.transform.position.z  // Keep the same Z position for 2D
            );
        }

        // Update the TextMeshPro text to show which shape to pick
        randomLetterText.text = correctShape.name;

    }
}