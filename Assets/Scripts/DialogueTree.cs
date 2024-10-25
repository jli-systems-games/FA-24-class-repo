using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Section", menuName = "Dialogue/Dialogue Section")]
public class DialogueTree : ScriptableObject
{
    public string[] NPCDialogue;

    public List<GameObject> playerOptions;
}
