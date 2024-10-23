using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomizer : MonoBehaviour
{
    public List<NPC> npcPool = new List<NPC>();
    public GameObject npcPrefab;
    public Transform spawnLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            SpawnNPCs();
        }
    }

    void SpawnNPCs()
    {
        GameObject tempNPC = Instantiate(npcPrefab, spawnLocation);
        NPCController controller = tempNPC.GetComponent<NPCController>();

        int randomIndex = Random.Range(0, npcPool.Count);
        while (randomIndex >= npcPool.Count)
            randomIndex = Random.Range(0, npcPool.Count);


        controller.npcInfo = npcPool[randomIndex];
        /*
        int currentCount = npcPool.Count;

        for (int i = 0; i < currentCount; i++)
        {
            if (i != randomIndex)
            {
                npcPool.Add(npcPool[i]);
            }
        }*/

        npcPool.RemoveAt(randomIndex);
    }

}
