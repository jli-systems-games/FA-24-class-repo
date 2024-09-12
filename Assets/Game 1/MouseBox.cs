using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBox : MonoBehaviour
{
    public GameObject player;

    public float spawn;
    public List<Transform> spawnLocations = new List<Transform>();

    public void Start()
    {
        player.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
    }

    public void Begin()
    {
        player.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
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
