using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppleBaskets : MonoBehaviour
{
    public GameObject goal;

    public float spawn;
    public List<Transform> spawnLocations = new();

    public GameObject player;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = player.transform.position;
        goal.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
    }

    public void Win()
    {
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }

    public void Lose()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
