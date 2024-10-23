using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCJob
{
    Baker,
    Farmer,
    Gardener
}

[CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPC", order = 0)]
public class NPC : ScriptableObject
{
    public string npcName = "PLACEHOLDER";
    public int npcStr = 10;
    public NPCJob npcJob = NPCJob.Baker;
}
