using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner_Door : MonoBehaviour
{
    public Transform connectingTransform;
    public List<GameObject> roomList = new List<GameObject>();
    public bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;

        if (DungeonSpawner.spawnedRooms.Count < 100)
        {
            GameObject dungeonSpawner = GameObject.FindGameObjectWithTag("DungeonSpawner");
            int temp = Random.Range(0, roomList.Count);
            while (temp >= roomList.Count)
            {
                temp = Random.Range(0, roomList.Count);
            }

            //GameObject tempRoom = Instantiate(roomList[temp], connectingTransform);
            GameObject tempRoom = Instantiate(roomList[temp]);
            tempRoom.transform.position = connectingTransform.position;
            //Debug.Log("1: "+ tempRoom.transform.localPosition);
            //tempRoom.transform.SetParent(dungeonSpawner.transform,true);
            //Debug.Log("2: " + tempRoom.transform.localPosition);

            foreach (Vector3 spawnedRoom in DungeonSpawner.spawnedRooms)
            {
                if (spawnedRoom == tempRoom.transform.localPosition) 
                {
                    Destroy(tempRoom);
                    canSpawn = false;
                }
                    
            }

            if (canSpawn == true)
            {
                DungeonSpawner.spawnedRooms.Add(tempRoom.transform.localPosition);
                //Debug.Log(tempRoom.transform.localPosition);
            }
        }
    }


}
