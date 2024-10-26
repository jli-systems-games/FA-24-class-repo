using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


[System.Serializable]
public class KnifeCustomization
{
    public Sprite baseKnife;               // Base knife sprite
    public List<Sprite> colorOverlays;      // List of color overlays for this knife
    public List<Sprite> engravingOverlays;  // List of engraving overlays for this knife
}

public class CustomizeManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject knifeSelectionPanel;    // Panel for knife selection
    public GameObject customizationPanel;     // Panel for color/engraving customization

    [Header("Knife Customization Options")]
    public List<KnifeCustomization> knives;   // List of knives with their specific overlays

    private int selectedKnifeIndex = 0;       // Keeps track of the selected knife
    private int selectedColorIndex = 0;       // Keeps track of the selected color overlay
    private int selectedEngravingIndex = 0;   // Keeps track of the selected engraving overlay

    [Header("Customization UI Elements")]
    public Image knifePreview;                // Base knife preview
    public Image colorOverlay;                // Image to display the color overlay
    public Image engravingOverlay;            // Image to display the engraving overlay

    public Button[] colorButtons;             // Buttons for choosing color overlays
    public Button[] engravingButtons;         // Buttons for choosing engraving overlays
    public Button confirmButton;              // Confirm button for finalizing selection
    public Button nextButton;                 // Button to move to customization panel
    public Button[] knifeButtons;             // Buttons for selecting knives
    public Button backButton;                 // Button to go back to knife selection

    void Start()
    {
        ShowKnifeSelectionPanel();  // Start with knife selection
        InitializeButtons();        // Set up button listeners
        SetupKnifeButtons();        // Setup knife selection buttons

        // Initialize the preview with the first knife in the list
        selectedKnifeIndex = 0;      // Ensure we're showing the first knife
        UpdatePreview();             // Display the first knife in the preview
        UpdateCustomizationButtons(); // Set up the customization buttons for the first knife
    }

    private void InitializeButtons()
    {
        // Initialize color and engraving buttons with listeners
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i; // Capture the index for the listener
            colorButtons[i].onClick.AddListener(() => OnColorButtonClick(index));
        }

        for (int i = 0; i < engravingButtons.Length; i++)
        {
            int index = i; // Capture the index for the listener
            engravingButtons[i].onClick.AddListener(() => OnEngravingButtonClick(index));
        }

        // Add listeners to confirm, next, and back buttons
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);  // Set up Back button listener
    }

    private void SetupKnifeButtons()
    {
        for (int i = 0; i < knives.Count; i++)
        {
            int index = i; // Capture index for the listener
            knifeButtons[i].onClick.AddListener(() => OnKnifeButtonClick(index));
        }
    }

    // Knife Selection
    public void OnKnifeButtonClick(int knifeIndex)
    {
        Debug.Log($"Knife button clicked: {knifeIndex}"); // Debugging log

        // Update selected knife index
        selectedKnifeIndex = knifeIndex;

        // Reset color and engraving indices to defaults for the new knife
        selectedColorIndex = 0;
        selectedEngravingIndex = 0;

        // Update the knife preview with the base knife sprite
        UpdatePreview(); // Ensure preview is updated immediately

        // Update the customization buttons for the selected knife
        UpdateCustomizationButtons();

        // Log selection for debugging
        Debug.Log($"Knife {selectedKnifeIndex + 1} selected with base sprite: {knifePreview.sprite.name}");
    }

    // Moves to the customization section after selecting a knife
    public void OnNextButtonClick()
    {
        knifeSelectionPanel.SetActive(false); // Hide knife selection panel
        customizationPanel.SetActive(true);   // Show customization options

        // Ensure the preview reflects the selected knife
        UpdatePreview();
    }

    // Method for Back Button to return to knife selection
    public void OnBackButtonClick()
    {
        customizationPanel.SetActive(false); // Hide customization panel
        knifeSelectionPanel.SetActive(true); // Show knife selection panel
    }

    // Color Selection (handles sprite overlay for color)
    public void OnColorButtonClick(int colorIndex)
    {
        selectedColorIndex = colorIndex;
        UpdatePreview(); // Update knife preview color
    }

    // Engraving Selection (handles sprite overlay for engraving)
    public void OnEngravingButtonClick(int engravingIndex)
    {
        selectedEngravingIndex = engravingIndex;
        UpdatePreview(); // Update knife preview with engraving
    }

    // Finalize and confirm selection
    public void OnConfirmButtonClick()
    {
        // Handle confirmation logic here (saving choices, proceeding to gameplay, etc.)
        Debug.Log($"Knife selected: {selectedKnifeIndex}, Color Overlay: {selectedColorIndex}, Engraving Overlay: {selectedEngravingIndex}");
        SceneManager.LoadScene("Game Scene");
    }

    private void ShowKnifeSelectionPanel()
    {
        knifeSelectionPanel.SetActive(true);
        customizationPanel.SetActive(false); // Ensure the customization panel is hidden at start
    }

    private void UpdatePreview()
    {
        // Update the base knife sprite
        knifePreview.sprite = knives[selectedKnifeIndex].baseKnife;

        // Set the color and engraving overlays for the selected knife
        colorOverlay.sprite = knives[selectedKnifeIndex].colorOverlays[selectedColorIndex];
        engravingOverlay.sprite = knives[selectedKnifeIndex].engravingOverlays[selectedEngravingIndex];
    }

    private void UpdateCustomizationButtons()
    {
        // Clear existing listeners from color and engraving buttons to avoid duplicates
        foreach (var button in colorButtons) button.onClick.RemoveAllListeners();
        foreach (var button in engravingButtons) button.onClick.RemoveAllListeners();

        // Set up color buttons based on the selected knife's color overlays
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i; // Capture the index to use inside the lambda
            if (i < knives[selectedKnifeIndex].colorOverlays.Count)
            {
                colorButtons[i].gameObject.SetActive(true); // Show button if overlay exists
                colorButtons[i].onClick.AddListener(() => OnColorButtonClick(index));
            }
            else
            {
                colorButtons[i].gameObject.SetActive(false); // Hide unused buttons
            }
        }

        // Set up engraving buttons based on the selected knife's engraving overlays
        for (int i = 0; i < engravingButtons.Length; i++)
        {
            int index = i; // Capture the index to use inside the lambda
            if (i < knives[selectedKnifeIndex].engravingOverlays.Count)
            {
                engravingButtons[i].gameObject.SetActive(true); // Show button if overlay exists
                engravingButtons[i].onClick.AddListener(() => OnEngravingButtonClick(index));
            }
            else
            {
                engravingButtons[i].gameObject.SetActive(false); // Hide unused buttons
            }
        }
    }
}