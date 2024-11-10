using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public GameObject islandPrefab;
    public Material[] islandMaterials;

    public int maxIslands = 4;
    public float spawnDistanceBelow = 0.2f;
    public float spawnDistanceFront = 4f;

    private Queue<GameObject> islandPool = new Queue<GameObject>();
    private Transform player;
    private Jump playerJump;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerJump = player.GetComponent<Jump>();

        // Subscribe to the Jumped event.
        if (playerJump != null)
        {
            playerJump.Jumped += OnPlayerJump;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (playerJump != null)
        {
            playerJump.Jumped -= OnPlayerJump;
        }
    }

    private void OnPlayerJump()
    {
        if (playerJump.JumpCount == 1 || playerJump.JumpCount == 2)
        {
            SpawnIsland();
        }

        if (islandPool.Count > maxIslands)
        {
            RemoveOldestIsland();
        }
    }

    void SpawnIsland()
    {
        Vector3 spawnPosition = player.position
            + player.forward * spawnDistanceFront
            - new Vector3(0, spawnDistanceBelow, 0);

        GameObject newIsland = Instantiate(islandPrefab, spawnPosition, Quaternion.identity);

        Renderer islandRenderer = newIsland.GetComponent<Renderer>();
        if (islandRenderer != null && islandMaterials.Length > 0)
        {
            islandRenderer.material = islandMaterials[Random.Range(0, islandMaterials.Length)];
        }

        islandPool.Enqueue(newIsland);
    }

    void RemoveOldestIsland()
    {
        GameObject oldIsland = islandPool.Dequeue();
        Destroy(oldIsland);
    }
}
