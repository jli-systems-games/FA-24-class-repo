using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoWest : MonoBehaviour
{
    public GameObject player;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = player.transform.position;
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
