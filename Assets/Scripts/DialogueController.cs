using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueDisplay;
    public DialogueTree nextTree;

    private int dialogueIndex;
    private int sectIndex;

    private GameManager _gameManager;

    public GameObject continueButton;

    public Transform[] transforms;

    private GameObject[] prevChoices;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        sectIndex = 0;

        BeginSect();
    }

    // Update is called once per frame
    public void BeginSect()
    {
        dialogueIndex = 0;
        prevChoices = GameObject.FindGameObjectsWithTag("ChoiceButton");
        if (prevChoices != null)
        {
            for (int i = 0; i < prevChoices.Length; i++)
            {
                Destroy(prevChoices[i]);
            }
        }
        continueButton.SetActive(true);
    }

    public void NextLine()
    {
        if (dialogueIndex <= nextTree.NPCDialogue.Length)
        {
            dialogueDisplay.text = nextTree.NPCDialogue[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            for (int i = 0; i < nextTree.playerOptions.Count; i++) 
            {
                Instantiate(nextTree.playerOptions[i], transforms[i]);
            }
            continueButton.SetActive(false);
        }
    }
}
