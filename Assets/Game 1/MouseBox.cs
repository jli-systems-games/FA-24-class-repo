using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MouseBox : MonoBehaviour
{
    public GameObject player;

    public float spawn;
    public List<Transform> spawnLocations = new();

    public void Start()
    {
        player.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
    }

    public void Win()
    {
        TotalScore.currentScore++;

        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }

    public void Lose()
    {
        TotalScore.currentScore = 0;

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
