using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBaskets : MonoBehaviour
{
    public GameObject goal;

    public float spawn;
    public List<Transform> spawnLocations = new List<Transform>();

    public GameObject player;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = player.transform.position;
        goal.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
    }

    public void Begin()
    {
        player.transform.position = startPosition;
        goal.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
    }

    public void Win()
    {
        Begin();
    }

    public void Lose()
    {
        Begin();
    }
}
