using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject food;   

    public void SpawnSlimeFood()
    {
        int randomInt = Random.Range(0, 4);

        for (int i = 0; i < randomInt + 1; i++)
        {
            float randomRotation = Random.Range(0, 360);
            Instantiate(food, transform.position, Quaternion.Euler(0, 0, randomRotation));
        }
    }

}
