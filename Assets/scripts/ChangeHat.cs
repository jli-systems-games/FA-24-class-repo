using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class ChangeHat : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsInScene; // List of objects in the scene to be toggled
    [SerializeField] private List<int> fashionValues; // List of fashion values for each object
    [SerializeField] private List<int> moodValues; // List of mood values for each object
    private int currentIndex = 0; // Keep track of the current active object

    [SerializeField] private Button toggleButton; // Reference to the UI button
    [SerializeField] private Slider fashionSlider; // Reference to the fashion slider
    [SerializeField] private AnimationController animationController; // Reference to the AnimationController script

    private int maxFashion = 10; // Maximum fashion value
    private int currentFashionValue = 3; // Default fashion value

    void Start()
    {
        // Set up the fashion slider
        fashionSlider.maxValue = maxFashion;
        fashionSlider.value = currentFashionValue;

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

        // Save the current object's fashion and mood values
        int previousFashionValue = fashionValues[currentIndex];
        int previousMoodValue = moodValues[currentIndex];

        // Move to the next object
        currentIndex = (currentIndex + 1) % objectsInScene.Count;

        // Activate the next object
        objectsInScene[currentIndex].SetActive(true);

        // Get the new object's fashion and mood values
        int newFashionValue = fashionValues[currentIndex];
        int newMoodValue = moodValues[currentIndex];

        // Adjust the fashion slider based on the difference
        UpdateFashionSlider(newFashionValue - previousFashionValue);

        // Update mood value in the AnimationController script
        UpdateMood(newMoodValue - previousMoodValue);
    }

    // Function to update the fashion slider
    private void UpdateFashionSlider(int fashionDifference)
    {
        // Update the current fashion value
        currentFashionValue += fashionDifference;

        // Ensure the value stays within the valid range
        currentFashionValue = Mathf.Clamp(currentFashionValue, 0, maxFashion);

        // Update the slider value
        fashionSlider.value = currentFashionValue;
    }

    // Function to update mood in the AnimationController script
    private void UpdateMood(int moodDifference)
    {
        // Call the AnimationController's method to update the mood
        if (moodDifference > 0)
        {
            for (int i = 0; i < moodDifference; i++)
            {
                animationController.IncreaseMood();
            }
        }
        else
        {
            for (int i = 0; i < Mathf.Abs(moodDifference); i++)
            {
                animationController.DecreaseMood();
            }
        }
    }
}
