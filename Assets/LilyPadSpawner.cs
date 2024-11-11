using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadSpawner : MonoBehaviour
{
    public List<LilyPad> lilyPadList = new List<LilyPad>();
    public GameObject lilyPadPrefab;

    public void RegisterLilyPad(LilyPad lilyPad)
    {
        lilyPadList.Add(lilyPad);
    }

    public void UpdateLilyPads(GameObject currentLilyPad)
    {
        for (int i = lilyPadList.Count - 1; i >= 0; i--)
        {
            if (!lilyPadList[i].clicked)
            {
                Destroy(lilyPadList[i].gameObject);
                lilyPadList.RemoveAt(i);
            }
        }

        Vector3 spawnPosition = currentLilyPad.transform.position;
        SpawnNewLilyPads(spawnPosition);
    }

    private void SpawnNewLilyPads(Vector3 position)
    {
        int numLilyPads = 3;
        float spawnRange = 50f;

        for (int i = 0; i < numLilyPads; i++)
        {
            float randomX = Random.Range(position.x - spawnRange, position.x + spawnRange);
            float randomY = Random.Range(position.y - spawnRange, position.y + spawnRange);
            Vector3 spawnPos = new Vector3(randomX, randomY, position.z);

            GameObject newLilyPad = Instantiate(lilyPadPrefab, spawnPos, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            RegisterLilyPad(newLilyPad.GetComponent<LilyPad>());
        }
    }
}
