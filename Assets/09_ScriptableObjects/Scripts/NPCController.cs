using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPC npcInfo;
    // Start is called before the first frame update
    void Start()
    {
        SO_GameManager.onNPCLoad += SetUp;
    }

    public void SetUp()
    {
        npcInfo = Data.dataNPCs["John"];
    }
}
