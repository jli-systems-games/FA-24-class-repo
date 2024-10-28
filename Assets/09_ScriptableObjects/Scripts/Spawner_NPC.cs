using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_NPC : MonoBehaviour
{
    public List<string> npcList = new List<string>();
    public GameObject npcPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int temp = Random.Range(0, npcList.Count);
        while (temp >= npcList.Count)
            temp = Random.Range(0, npcList.Count);

        GameObject tempNPC = Instantiate(npcPrefab, transform);

        tempNPC.GetComponent<ProcGen_NPCController>().randomNPC = npcList[temp];
        tempNPC.GetComponent<ProcGen_NPCController>().Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
