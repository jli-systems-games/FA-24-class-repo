using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateLevel : MonoBehaviour
{
    /*
    public GameObject platform1;
    //public GameObject platform2;
    //public GameObject platform3;
    //public GameObject platform4;
    //public GameObject platform5;

    public BoxCollider2D platformCollider;
    public float screenRight;
    public float groundRight;
    bool didGenerateGround;

    //Player player;
    
    private void Awake()
    {

        screenRight = Camera.main.transform.position.x * 2;
    }

    private void FixedUpdate()
    {
        groundRight = transform.position.x + (platformCollider.size.x / 2);
        if (!didGenerateGround)
        {
            if(groundRight < screenRight)
            {
                didGenerateGround = true;
                genPlatform();
            }
        }
        
    }

    void genPlatform()
    {
        GameObject newPlatform = Instantiate(platform1);
        BoxCollider2D platformCollider = newPlatform.GetComponent<BoxCollider2D>();
        Vector2 pos;
        pos.x = screenRight + 30;
        pos.y = Random.Range(-6, 5);
        newPlatform.transform.position = pos;
    }

}
*/
    public GameObject[] platformPrefabs;
    public GameObject platformPrefab;  // Platform prefab to instantiate
    public Transform player;           // Reference to the player's transform
    public float spawnDistance = 10f;  // Distance ahead of the player to spawn platforms
    public float minY = -2f;           // Minimum y position for platforms
    public float maxY = 2f;            // Maximum y position for platforms
    public float minXDistance = 5f;    // Minimum horizontal distance between platforms
    public float maxXDistance = 8f;    // Maximum horizontal distance between platforms
    public float destroyOffset = 2f;   // Extra space past the camera to destroy platforms

    private float lastSpawnX;          // Last x position where a platform was spawned
    private List<GameObject> platforms = new List<GameObject>();  // List to track spawned platforms

    private void Start()
    {
        lastSpawnX = player.position.x;  // Initialize starting spawn position
        GeneratePlatform();              // Generate the first platform
    }

    private void Update()
    {
        // Only spawn a platform if the player is far enough to the right of the last spawn position
        if (player.position.x + spawnDistance > lastSpawnX)
        {
            GeneratePlatform();
        }

        // Check for and destroy platforms that have moved past the camera's view on the left
        DestroyOffscreenPlatforms();
    }

    void GeneratePlatform()
    {
        platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        // Random y position within range
        float yPos = Random.Range(minY, maxY);

        // Random x distance between platforms
        float xDistance = Random.Range(minXDistance, maxXDistance);
        float xPos = lastSpawnX + xDistance;

        // Instantiate the platform at the calculated position
        Vector2 spawnPosition = new Vector2(xPos, yPos);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Add the platform to the list for tracking
        platforms.Add(newPlatform);

        // Update the last spawn x position
        lastSpawnX = xPos;
    }

    void DestroyOffscreenPlatforms()
    {
        // Calculate the leftmost position for the camera to start destroying platforms
        float cameraLeftEdge = Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) - destroyOffset;

        // Iterate through the list of platforms to find and destroy those past the left side
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.x < cameraLeftEdge)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);  // Remove it from the list
            }
        }
    }
}