using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSouth : MonoBehaviour
{
    public GameObject player;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = player.transform.position;
    }

    public void Begin()
    {
        player.transform.position = startPosition;
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
