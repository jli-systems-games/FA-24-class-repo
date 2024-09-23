using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorites : MonoBehaviour
{
    //player location
    public Transform player;

    //spawn hight above player y value
    //public Vector3 spawnHeight;

    //cube size
    public GameObject cubePrefab;
    //public Vector3 cubeSize = new(2, 2, 2);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMeteorites());
    }

    IEnumerator SpawnMeteorites()
    {
        while (true)
        {
            SpawnMeteorite();
            yield return new WaitForSeconds(.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMeteorite()
    {
        //spawn location
        int randomX = Random.Range(-5, 6);
        Vector3 spawnPosition = new(randomX, player.position.y + 45, 0);

        ////spawn cubes
        GameObject cube;

        cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = spawnPosition;
        //cube.transform.localScale = cubeSize;

        //physics
        //Rigidbody rb = cube.AddComponent<Rigidbody>();
        //rb.mass = 1f;
    }

}
