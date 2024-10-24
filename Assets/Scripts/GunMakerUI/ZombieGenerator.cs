using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject zombie;
    private float waitTime = 3;
    private float miniWaitTime = 0.9f;
    private void Start()
    {
        StartCoroutine(GenerateZombie());
    }

    IEnumerator GenerateZombie()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(zombie,transform.position,Quaternion.identity);
        if (waitTime > miniWaitTime)
        {
            waitTime -= 0.1f;
        }
        StartCoroutine(GenerateZombie());
    }
}
