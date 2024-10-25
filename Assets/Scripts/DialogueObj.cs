using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue Object", menuName = "Dialogue/Dialogue Object")]
public class DialogueObj : ScriptableObject
{
    public PersonalityTrait dialogueTrait;
    public string dialogueOption;
}
