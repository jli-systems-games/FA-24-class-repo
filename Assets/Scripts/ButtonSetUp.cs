using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetUp : MonoBehaviour
{
    public DialogueObj choice;
    public Button button;
    public TextMeshProUGUI buttonText;

    public DialogueTree nextDialogue;
    public DialogueController dialogueController;

    public PersonalityTrait assosciatedTrait;

    //public GameObject buttonPrefab;
    void Awake()
    {
        dialogueController = FindObjectOfType<DialogueController>();
        button = gameObject.GetComponent<Button>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        buttonText.text = choice.dialogueOption;
        assosciatedTrait = choice.dialogueTrait;
    }

    void PickOption()
    {
        dialogueController.nextTree = nextDialogue;
    }
}
