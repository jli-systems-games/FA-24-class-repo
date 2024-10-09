using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public TMP_Dropdown undertoneDropdown;

    private List<string> dialogueLines;
    private List<List<GameObject>> uiElementDisplay;

    private int currentLineIndex = 0;

    void Start()
    {
        dialogueLines = new List<string>
        {
            "Welcome to your color analysis session! Find out which season best suits you!\n\n(This is just for fun. Virtual tools are less accurate than in person color draping.)",
            "The four seasons are differentiated by color temperature (warm or cool), value (light or darkness), and chroma (muted or bright). Your ideal season is determined by your skin, hair, and eye color.\n\nSPRING\nwarm + light  →  bright\nSUMMER\ncool + light  →  muted\nAUTUMN\nwarm + dark  →  muted‍\nWINTER\ncool + dark  →  bright",
            "First, are you warm or cool toned? Please place your face within the oval.\n \nA clashing result can make the skin look green or sickly."
        };

        uiElementDisplay = new List<List<GameObject>>()
        {
            new List<GameObject> { GameObject.Find("next button") },
            new List<GameObject> { GameObject.Find("next button"), GameObject.Find("seasonal palettes") },
            new List<GameObject> { GameObject.Find("next button"), GameObject.Find("webcam image"), GameObject.Find("undertone") }

        };

        UpdateUI();
        nextButton.onClick.AddListener(AdvanceDialogue);
        undertoneDropdown.onValueChanged.AddListener(DropdownSelection);
    }

    public void AdvanceDialogue()
    {
        if (currentLineIndex < dialogueLines.Count - 1)
        {
            currentLineIndex++;
            UpdateUI();
        }
        else
        {
            Debug.Log("End of dialogue");
        }
    }

    private void UpdateUI()
    {
        dialogueText.text = dialogueLines[currentLineIndex];

        foreach (var uiElements in uiElementDisplay)
        {
            foreach (GameObject element in uiElements)
            {
                element.SetActive(false);
            }
        }

        if (currentLineIndex < uiElementDisplay.Count)
        {
            foreach (GameObject element in uiElementDisplay[currentLineIndex])
            {
                element.SetActive(true);
            }
        }
    }

    private void DropdownSelection(int index)
    {
        dialogueLines.Clear();

        if (undertoneDropdown.options[index].text == "cool")
        {
            dialogueLines.Add("You selected cool tones! Here are some recommendations...");
        }
        else if (undertoneDropdown.options[index].text == "warm")
        {
            dialogueLines.Add("You selected warm tones! Here are some recommendations...");
        }

        currentLineIndex = 0;
        UpdateUI();
    }
}