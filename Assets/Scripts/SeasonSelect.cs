using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeasonSelect : MonoBehaviour
{
    public TMP_Dropdown undertoneDropdown;
    public TMP_Dropdown contrastDropdown;
    public TextMeshProUGUI seasonResultText;
    public Button assignSeasonButton;

    private string selectedUndertone;
    private string selectedContrast;

    void Start()
    {
        undertoneDropdown.onValueChanged.AddListener(delegate { ToneSelectionChanged(); });
        contrastDropdown.onValueChanged.AddListener(delegate { ContrastSelectionChanged(); });
        assignSeasonButton.onClick.AddListener(AssignSeason);

        undertoneDropdown.ClearOptions();
        contrastDropdown.ClearOptions();

        List<string> undertoneOptions = new List<string> { "my undertone is:", "warm", "cool" };
        List<string> contrastOptions = new List<string> { "my contrast is:", "low", "medium", "high" };

        undertoneDropdown.AddOptions(undertoneOptions);
        contrastDropdown.AddOptions(contrastOptions);

        assignSeasonButton.interactable = false;
    }

    private void ToneSelectionChanged()
    {
        selectedUndertone = undertoneDropdown.options[undertoneDropdown.value].text;
        CheckSelections();
    }

    private void ContrastSelectionChanged()
    {
        selectedContrast = contrastDropdown.options[contrastDropdown.value].text;
        CheckSelections();
    }

    private void CheckSelections()
    {
        if (undertoneDropdown.value != 0 && contrastDropdown.value != 0)
        {
            assignSeasonButton.interactable = true;
        }
        else
        {
            assignSeasonButton.interactable = false;
        }
    }

    private void AssignSeason()
    {
        string assignedSeason = "";

        // Assign season based on tone and contrast
        if (selectedUndertone == "cool")
        {
            if (selectedContrast == "low") assignedSeason = "Summer";
            else if (selectedContrast == "medium") assignedSeason = "Summer";
            else if (selectedContrast == "high") assignedSeason = "Winter";
        }
        else if (selectedUndertone == "warm")
        {
            if (selectedContrast == "low") assignedSeason = "Autumn";
            else if (selectedContrast == "medium") assignedSeason = "Autumn";
            else if (selectedContrast == "high") assignedSeason = "Spring";
        }

        // Display the result
        seasonResultText.text = "Your season is: " + assignedSeason;
    }
}
