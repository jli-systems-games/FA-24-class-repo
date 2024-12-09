
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dialogue node", menuName = "dialogue")]
public class DialogueNodes : ScriptableObject
{
    public string dialogueText;
    public DialogueNodes nextNode;
}
