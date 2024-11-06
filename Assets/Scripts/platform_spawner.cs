using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_spawner : MonoBehaviour
{
    private Transform spawnPosition;

    private int index;

    private GameObject platform;

    private GameManager gameManager;

    public GameObject endPlatform;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawnPosition = this.transform;

        index = Random.Range(0,gameManager.platformSections.Count);

        if (gameManager.platformSections.Count > 0)
        {
            platform = gameManager.platformSections[index];
        }
        else
        {
            platform = endPlatform;
        }

        Instantiate(platform, spawnPosition);

        gameManager.platformSections.RemoveAt(index);

        Debug.Log("index: " + index);
        Debug.Log(platform.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
