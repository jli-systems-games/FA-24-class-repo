using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    private List<string> dialogueLines;
    private List<List<GameObject>> uiElementDisplay;

    private int currentLineIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogueLines = new List<string>
        {
            "Welcome to your color analysis session! Find out which season best suits you!\n\n(This is just for fun. Virtual tools are less accurate than in person color draping.)",
            "The four seasons are differentiated by color temperature (warm or cool), value (light or darkness), and chroma (muted or bright). Your ideal season is determined by your skin, hair, and eye color.\n\nSPRING\nwarm + light  →  bright\nSUMMER\ncool + light  →  muted\nAUTUMN\nwarm + dark  →  muted‍\nWINTER\ncool + dark  →  bright",
            "First, are you warm or cool toned? Please place your face within the oval.\n \nA clashing result can emphasize blemishes and eyebags, or make the skin look green or sickly.",
            "Are you high or low contrast? Assign features of your face (skin, hair, eyes, and mouth) a value of 1, 2, or 3 for light, medium, or dark.\n\n\n\n\n\n\n\n\n\n "
            //"Please select your undertone and contrast level."
        };

        uiElementDisplay = new List<List<GameObject>>()
        {
            new List<GameObject> { GameObject.Find("next button") },
            new List<GameObject> { GameObject.Find("next button"), GameObject.Find("seasonal palettes") },
            new List<GameObject> { GameObject.Find("next button"), GameObject.Find("undertone"), GameObject.Find("webcam image") },
            new List<GameObject> { GameObject.Find("next button"), GameObject.Find("contrast") }
            //new List<GameObject> { GameObject.Find("selection"), GameObject.Find("season select") }
        };

        UpdateUI();
        nextButton.onClick.AddListener(AdvanceDialogue);
    }

    // Update is called once per frame
    void Update()
    {

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
}