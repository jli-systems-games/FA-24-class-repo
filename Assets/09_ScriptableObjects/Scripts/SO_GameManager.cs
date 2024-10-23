using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_GameManager : MonoBehaviour
{
    public List<NPC> npcList = new List<NPC>();

    //Actions
    public static event Action onNPCLoad;

    // Start is called before the first frame update
    void Start()
    {
        LoadNPCData();
        Debug.Log(Data.dataNPCs.Values.Count);

        onNPCLoad?.Invoke();
    }

    void LoadNPCData()
    {
        foreach(NPC npc in npcList) 
        { 
            Data.dataNPCs.Add(npc.npcName, npc);
        }
    }
}
