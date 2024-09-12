using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }

    public void Win()
    {
        //Begin();
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }
}
