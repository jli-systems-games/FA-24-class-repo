using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject spawnObject;
    public List<GameObject> spawnObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int temp = Random.Range(0, spawnObject.Count);
        while(temp >= spawnObject.Count)
            temp = Random.Range(0, spawnObject.Count);

        Instantiate(spawnObject[temp],transform);
    }

}
