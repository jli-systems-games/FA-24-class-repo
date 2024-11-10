using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    [Header("Island Generation")]
    public GameObject islandPrefab;
    public Material[] islandMaterials;

    public int maxIslands = 4;
    public float spawnDistanceBelow = 0.2f;
    public float spawnDistanceFront = 4f;

    [Header("Decoration Generation")]
    public GameObject[] decorationPrefabs;
    public int[] decorationWeights;
    public int minDecorations = 1;
    public int maxDecorations = 5;
    public float safeMargin = 0.1f;

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

        // Randomize the island material
        Renderer islandRenderer = newIsland.GetComponent<Renderer>();
        if (islandRenderer != null && islandMaterials.Length > 0)
        {
            Material randomMaterial = islandMaterials[Random.Range(0, islandMaterials.Length)];
            islandRenderer.material = randomMaterial;

            RandomizeDecorations(newIsland, randomMaterial);
        }

        islandPool.Enqueue(newIsland);
    }

    void RandomizeDecorations(GameObject island, Material islandMaterial)
    {
        Collider islandCollider = island.GetComponent<Collider>();
        if (islandCollider == null)
            return;

        int decorationCount = Random.Range(minDecorations, maxDecorations + 1);

        for (int i = 0; i < decorationCount; i++)
        {
            GameObject decoration = GetRandomDecoration();

            Vector3 randomPosition = GetRandomSafePosition(islandCollider);

            GameObject spawnedDecoration = Instantiate(decoration, randomPosition, Quaternion.identity, island.transform);

            Renderer decorationRenderer = spawnedDecoration.GetComponent<Renderer>();
            if (decorationRenderer != null)
            {
                decorationRenderer.material = islandMaterial;
            }
        }
    }

    GameObject GetRandomDecoration()
    {
        int totalWeight = 0;
        foreach (int weight in decorationWeights)
        {
            totalWeight += weight;
        }

        // Get a random number and select the decoration based on the weight
        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;
        for (int i = 0; i < decorationPrefabs.Length; i++)
        {
            cumulativeWeight += decorationWeights[i];
            if (randomWeight < cumulativeWeight)
            {
                return decorationPrefabs[i];
            }
        }
        return decorationPrefabs[0];
    }

    Vector3 GetRandomSafePosition(Collider islandCollider)
    {
        Vector3 randomPosition = islandCollider.bounds.center
            + new Vector3(Random.Range(-islandCollider.bounds.extents.x + safeMargin, islandCollider.bounds.extents.x - safeMargin),
                          0,  // You may want to adjust the Y axis here if your island is flat
                          Random.Range(-islandCollider.bounds.extents.z + safeMargin, islandCollider.bounds.extents.z - safeMargin));

        while (Physics.CheckSphere(randomPosition, 0.5f))
        {
            randomPosition = islandCollider.bounds.center
                + new Vector3(Random.Range(-islandCollider.bounds.extents.x + safeMargin, islandCollider.bounds.extents.x - safeMargin),
                              0,
                              Random.Range(-islandCollider.bounds.extents.z + safeMargin, islandCollider.bounds.extents.z - safeMargin));
        }

        return randomPosition;
    }

    void RemoveOldestIsland()
    {
        GameObject oldIsland = islandPool.Dequeue();
        Destroy(oldIsland);
    }
}
