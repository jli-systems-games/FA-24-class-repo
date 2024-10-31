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
    private int selectedColorIndex = -1;      // Keeps track of the selected color overlay (-1 for none)
    private int selectedEngravingIndex = -1;  // Keeps track of the selected engraving overlay (-1 for none)

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

    [Header("Audio")]
    public AudioSource buttonSound;           // Audio source for button click sound

    void Start()
    {
        ShowKnifeSelectionPanel();
        InitializeButtons();
        SetupKnifeButtons();

        selectedKnifeIndex = 0;
        UpdatePreview();
        UpdateCustomizationButtons();

        // Hide overlays initially
        colorOverlay.enabled = false;
        engravingOverlay.enabled = false;
    }

    private void PlayButtonSound()
    {
        if (buttonSound != null)
        {
            buttonSound.Play();
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i; // Capture the index for the listener
            colorButtons[i].onClick.AddListener(() => {
                OnColorButtonClick(index);
                PlayButtonSound(); // Ensure sound plays on color selection
            });
        }

        for (int i = 0; i < engravingButtons.Length; i++)
        {
            int index = i; // Capture the index for the listener
            engravingButtons[i].onClick.AddListener(() => {
                OnEngravingButtonClick(index);
                PlayButtonSound(); // Ensure sound plays on engraving selection
            });
        }

        confirmButton.onClick.AddListener(() => {
            OnConfirmButtonClick();
            PlayButtonSound();
        });
        nextButton.onClick.AddListener(() => {
            OnNextButtonClick();
            PlayButtonSound();
        });
        backButton.onClick.AddListener(() => {
            OnBackButtonClick();
            PlayButtonSound();
        });
    }

    private void SetupKnifeButtons()
    {
        for (int i = 0; i < knives.Count; i++)
        {
            int index = i;
            knifeButtons[i].onClick.AddListener(() => {
                OnKnifeButtonClick(index);
                PlayButtonSound();  // Play sound on knife selection
            });
        }
    }

    public void OnKnifeButtonClick(int knifeIndex)
    {
        selectedKnifeIndex = knifeIndex;
        selectedColorIndex = -1;
        selectedEngravingIndex = -1;
        colorOverlay.enabled = false;
        engravingOverlay.enabled = false;

        UpdatePreview();
        UpdateCustomizationButtons();
    }

    public void OnNextButtonClick()
    {
        knifeSelectionPanel.SetActive(false);
        customizationPanel.SetActive(true);
        UpdatePreview();
    }

    public void OnBackButtonClick()
    {
        customizationPanel.SetActive(false);
        knifeSelectionPanel.SetActive(true);
    }

    public void OnColorButtonClick(int colorIndex)
    {
        PlayButtonSound();
        selectedColorIndex = colorIndex;
        colorOverlay.sprite = knives[selectedKnifeIndex].colorOverlays[selectedColorIndex];
        colorOverlay.enabled = true;
        UpdatePreview();
    }

    public void OnEngravingButtonClick(int engravingIndex)
    {
        PlayButtonSound();
        selectedEngravingIndex = engravingIndex;
        engravingOverlay.sprite = knives[selectedKnifeIndex].engravingOverlays[selectedEngravingIndex];
        engravingOverlay.enabled = true;
        UpdatePreview();
    }

    public void OnConfirmButtonClick()
    {
        Sprite baseKnife = knives[selectedKnifeIndex].baseKnife;
        Sprite colorOverlay = selectedColorIndex >= 0 ? knives[selectedKnifeIndex].colorOverlays[selectedColorIndex] : null;
        Sprite engravingOverlay = selectedEngravingIndex >= 0 ? knives[selectedKnifeIndex].engravingOverlays[selectedEngravingIndex] : null;

        CustomizationData.instance.SetCustomization(selectedKnifeIndex, baseKnife, colorOverlay, engravingOverlay);
        SceneManager.LoadScene("Game Scene"); // Replace with actual scene name
    }

    private void ShowKnifeSelectionPanel()
    {
        knifeSelectionPanel.SetActive(true);
        customizationPanel.SetActive(false);
    }

    private void UpdatePreview()
    {
        knifePreview.sprite = knives[selectedKnifeIndex].baseKnife;
        colorOverlay.enabled = selectedColorIndex >= 0;
        engravingOverlay.enabled = selectedEngravingIndex >= 0;
    }

    private void UpdateCustomizationButtons()
    {
        foreach (var button in colorButtons) button.onClick.RemoveAllListeners();
        foreach (var button in engravingButtons) button.onClick.RemoveAllListeners();

        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i;
            if (i < knives[selectedKnifeIndex].colorOverlays.Count)
            {
                colorButtons[i].gameObject.SetActive(true);
                colorButtons[i].onClick.AddListener(() => OnColorButtonClick(index));
            }
            else
            {
                colorButtons[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < engravingButtons.Length; i++)
        {
            int index = i;
            if (i < knives[selectedKnifeIndex].engravingOverlays.Count)
            {
                engravingButtons[i].gameObject.SetActive(true);
                engravingButtons[i].onClick.AddListener(() => OnEngravingButtonClick(index));
            }
            else
            {
                engravingButtons[i].gameObject.SetActive(false);
            }
        }
    }
}