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

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        sectIndex = 0;
        dialogueIndex = 0;
    }

    // Update is called once per frame
    void BeginSect()
    {
        dialogueIndex = 0;

    }
}
