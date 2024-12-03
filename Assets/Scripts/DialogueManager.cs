using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject choicePanel;

    [Header("Choices")]
    [SerializeField] private Button[] choices; // Buttons for all possible choices
    private TextMeshProUGUI[] choiceTexts; // Text components for each button

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in scene!");
        }
        instance = this;

        // Initialize choiceTexts based on the number of buttons
        choiceTexts = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choiceTexts[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
            if (choiceTexts[i] == null)
            {
                Debug.LogError($"No TextMeshProUGUI found in choice button {choices[i].name}");
            }
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        // Handle continuing the story when no choices are available
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentStory.currentChoices.Count == 0) // Continue only if no choices are present
            {
                ContinueStory();
            }
        }

        // Handle selecting a choice with Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentStory.currentChoices.Count > 0) // Select a choice only if choices are available
            {
                SelectHighlightedChoice();
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        choicePanel.SetActive(false);
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // Enable the choice panel and iterate over choices
        if (currentChoices.Count > 0)
        {
            choicePanel.SetActive(true);

            for (int i = 0; i < choices.Length; i++)
            {
                if (i < currentChoices.Count)
                {
                    // Enable the button and set its text
                    choices[i].gameObject.SetActive(true);
                    choiceTexts[i].text = currentChoices[i].text;

                    // Remove existing listeners and add a new one for the current choice
                    int choiceIndex = i; // Copy the index to avoid closure issues
                    choices[i].onClick.RemoveAllListeners();
                    choices[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
                }
                else
                {
                    // Disable unused buttons
                    choices[i].gameObject.SetActive(false);
                }
            }

            // Automatically select the first button
            EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        }
        else
        {
            choicePanel.SetActive(false);
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        // Pass the selected choice index to the Ink story
        currentStory.ChooseChoiceIndex(choiceIndex);
        choicePanel.SetActive(false);
        ContinueStory();
    }

    private void SelectHighlightedChoice()
    {
        // Use the Event System to get the currently selected button
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton != null)
        {
            // Find the index of the selected button
            for (int i = 0; i < choices.Length; i++)
            {
                if (choices[i].gameObject == selectedButton)
                {
                    OnChoiceSelected(i);
                    return;
                }
            }
        }
    }
}
