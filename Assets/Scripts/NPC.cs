using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Characters/NPC")]
public class NPC : ScriptableObject
{
    public string charName;
    public GameObject gameObjectPrefab;
}
