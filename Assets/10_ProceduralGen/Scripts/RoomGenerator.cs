using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;

    public List<NPC> npcList = new List<NPC>();

    private void Start()
    {
        foreach(NPC npc in npcList)
        {
            Data.dataNPCs.Add(npc.npcName, npc);
        }

        Instantiate(roomPrefab);
    }

}
