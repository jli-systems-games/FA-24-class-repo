using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueDisplay;
    public DialogueTree nextTree;

    private int dialogueIndex;
    private int sectIndex;

    private GameManager _gameManager;

    public GameObject continueButton;
    public GameObject resetButton;

    public Transform[] transforms;

    private GameObject[] prevChoices;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        resetButton.SetActive(false);
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
        dialogueDisplay.text = nextTree.NPCDialogue[dialogueIndex];
    }

    public void NextLine()
    {
        if (dialogueIndex < nextTree.NPCDialogue.Length-1)
        {
            dialogueIndex++;
            dialogueDisplay.text = nextTree.NPCDialogue[dialogueIndex];
            
        }
        else if(nextTree.playerOptions != null)
        {
            for (int i = 0; i < nextTree.playerOptions.Count; i++) 
            {
                if (nextTree.playerOptions[i].GetComponent<ButtonSetUp>().assosciatedTrait == GameManager.trait1 || nextTree.playerOptions[i].GetComponent<ButtonSetUp>().assosciatedTrait == GameManager.trait2)
                {
                    
                    if (nextTree.playerOptions.Count <= 2)
                    {
                        GameObject choice = Instantiate(nextTree.playerOptions[i], transforms[i]);
                        choice.transform.SetParent(canvas.transform, false);
                    }
                    else if(i < 2)
                    {
                        GameObject choice = Instantiate(nextTree.playerOptions[i], transforms[0]);
                        choice.transform.SetParent(canvas.transform, false);
                    }
                    else if(i >= 2)
                    {
                        GameObject choice = Instantiate(nextTree.playerOptions[i], transforms[1]);
                        choice.transform.SetParent(canvas.transform, false);
                    }
                }
            }
            continueButton.SetActive(false);
        }
        else
        {
            resetButton.SetActive(true);
        }
    }

    public void Restart()
    {
        _gameManager.Start();
        SceneManager.LoadScene(0);
    }
}
