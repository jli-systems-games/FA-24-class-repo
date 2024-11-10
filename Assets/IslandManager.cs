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
    public int minDecorations = 2;
    public int maxDecorations = 6;
    public float safeMargin = 0.25f;

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
        SpawnIsland();

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
        // Ensure arrays are the same length
        if (decorationPrefabs.Length != decorationWeights.Length)
        {
            Debug.LogError("Decoration prefabs and weights arrays must have the same length.");
            return null;  // Or handle error accordingly.
        }

        // Calculate total weight
        int totalWeight = 0;
        foreach (int weight in decorationWeights)
        {
            totalWeight += weight;
        }

        // Handle case where total weight is zero
        if (totalWeight == 0)
        {
            Debug.LogWarning("Total weight is zero, returning default decoration.");
            return decorationPrefabs[0];
        }

        // Generate a random number between 0 and the total weight
        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Select decoration based on weighted probabilities
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
        // Increase the spread distance by adjusting the range.
        // You can make the range wider by modifying the extents and adding some more margin if needed.
        Vector3 randomPosition = islandCollider.bounds.center
            + new Vector3(
                Random.Range(-islandCollider.bounds.extents.x * 1.2f + safeMargin, islandCollider.bounds.extents.x * 1.2f - safeMargin),
                Random.Range(-islandCollider.bounds.extents.z * 1.2f + safeMargin, islandCollider.bounds.extents.z * 1.2f - safeMargin)
            );

        // Ensure the decoration does not overlap with others
        while (Physics.CheckSphere(randomPosition, 0.5f))
        {
            randomPosition = islandCollider.bounds.center
                + new Vector3(
                    Random.Range(-islandCollider.bounds.extents.x * 1.2f + safeMargin, islandCollider.bounds.extents.x * 1.2f - safeMargin),
                    0,
                    Random.Range(-islandCollider.bounds.extents.z * 1.2f + safeMargin, islandCollider.bounds.extents.z * 1.2f - safeMargin)
                );
        }

        return randomPosition;
    }


    void RemoveOldestIsland()
    {
        GameObject oldIsland = islandPool.Dequeue();
        Destroy(oldIsland);
    }
}
