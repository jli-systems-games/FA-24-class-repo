using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_NPCController : MonoBehaviour
{
    public NPC npcInfo;
    public string randomNPC;



    // Start is called before the first frame updat

    public void Setup()
    {
        npcInfo = Data.dataNPCs[randomNPC];
    }
}
