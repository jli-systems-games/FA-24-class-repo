using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    //public List <GameObject> roomList = new List <GameObject>();
    public GameObject starterRoom;
    public static List <Vector3> spawnedRooms = new List<Vector3>();  

    // Start is called before the first frame update
    void Start()
    {
        //spawnedRooms.Clear();
        GameObject tempRoom = Instantiate(starterRoom,transform);
        spawnedRooms.Add(tempRoom.transform.position);
    }

}
