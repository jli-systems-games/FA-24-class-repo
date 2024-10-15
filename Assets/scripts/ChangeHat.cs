using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class ChangeHat : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsInScene; // List of objects in the scene to be toggled
    private int currentIndex = 0; // Keep track of the current active object

    [SerializeField] private Button toggleButton; // Reference to the UI button

    void Start()
    {
        // Ensure all objects are deactivated at the start except the first one
        for (int i = 0; i < objectsInScene.Count; i++)
        {
            objectsInScene[i].SetActive(i == currentIndex); // Only the first object is active
        }

        // Add a listener to the button to call the ToggleNextObject function when clicked
        toggleButton.onClick.AddListener(ToggleNextObject);
    }

    // Function to toggle to the next object in the list
    private void ToggleNextObject()
    {
        // Deactivate the current object
        objectsInScene[currentIndex].SetActive(false);

        // Move to the next object
        currentIndex = (currentIndex + 1) % objectsInScene.Count;

        // Activate the next object
        objectsInScene[currentIndex].SetActive(true);
    }
}
