using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateLevel : MonoBehaviour
{
    public GameObject[] platformPrefabs; 
    public GameObject[] firstPlatformPrefabs;
    public GameObject playerPrefab;
    public float spawnDistance = 10f;
    public float minY = -2f;
    public float maxY = 2f;
    public float minXSpacing = 2f;
    public float maxXSpacing = 4f;
    public float destroyOffset = 5f;
    public float initialPlatformSpeed = 2f;
    public float speedIncreaseRate = 0.1f;
    public float maxPlatformSpeed = 10f;
    public float playerOffsetSpeed = 1f;

    public float maxXPosition = -340f;

    private float currentPlatformSpeed;
    private float lastSpawnX;
    private List<GameObject> platforms = new List<GameObject>();
    private float distanceMovedSinceLastSpawn = 0f;
    private bool firstPlatformSpawned = false;
    private GameObject player;

    private void Start()
    {
        currentPlatformSpeed = initialPlatformSpeed;
        lastSpawnX = 0f;
    
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity); 

        GeneratePlatform();                           
    }

    private void Update()
    {
        currentPlatformSpeed = Mathf.Min(currentPlatformSpeed + speedIncreaseRate * Time.deltaTime, maxPlatformSpeed);

        float playerInput = Input.GetAxis("Horizontal"); 

        if (Mathf.Abs(playerInput) < 0.1f)
        {
            player.transform.position += Vector3.left * (currentPlatformSpeed + playerOffsetSpeed) * Time.deltaTime;
        }
        else
        {
            float moveDirection = playerInput > 0 ? 1f : -1f;
            player.transform.position += Vector3.right * moveDirection * Time.deltaTime * 5f;
        }

        distanceMovedSinceLastSpawn += Mathf.Abs(player.transform.position.x - lastSpawnX);

        if (distanceMovedSinceLastSpawn >= spawnDistance && player.transform.position.x < maxXPosition)
        {
            GeneratePlatform();
            distanceMovedSinceLastSpawn = 0f;
        }

        foreach (GameObject platform in platforms)
        {
            platform.transform.position += Vector3.left * currentPlatformSpeed * Time.deltaTime;
        }

        DestroyOffscreenPlatforms();
    }

    void GeneratePlatform()
    {
        GameObject selectedPlatform;

        if (!firstPlatformSpawned)
        {
            selectedPlatform = firstPlatformPrefabs[Random.Range(0, firstPlatformPrefabs.Length)];
            firstPlatformSpawned = true;

            float platformWidth = selectedPlatform.GetComponent<Renderer>().bounds.size.x;
            float xPos = player.transform.position.x;
            float yPos = player.transform.position.y - player.GetComponent<Collider2D>().bounds.size.y / 2f - 0.1f;
            

            Vector2 spawnPosition = new Vector2(xPos, yPos);
            GameObject newPlatform = Instantiate(selectedPlatform, spawnPosition, Quaternion.identity);

            player.transform.position = new Vector3(xPos, yPos + player.GetComponent<Collider2D>().bounds.size.y / 2f, player.transform.position.z);

            platforms.Add(newPlatform);
            lastSpawnX = xPos;
        }
        else
        {
            selectedPlatform = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

            float platformWidth = selectedPlatform.GetComponent<Renderer>().bounds.size.x;

            float xSpacing = Random.Range(minXSpacing, maxXSpacing);
            float xDistance = platformWidth + xSpacing;

            float xPos = lastSpawnX + xDistance;
            float yPos = Random.Range(minY, maxY);

            Vector2 spawnPosition = new Vector2(xPos, yPos);
            GameObject newPlatform = Instantiate(selectedPlatform, spawnPosition, Quaternion.identity);

            platforms.Add(newPlatform);

            lastSpawnX = xPos;
        }
    }

    void DestroyOffscreenPlatforms()
    {
        float cameraLeftEdge = Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect);

        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.x < cameraLeftEdge - destroyOffset)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}