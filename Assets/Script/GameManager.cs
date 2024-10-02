using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    NavigationManager npcs;
    void Start()
    {
        npcs = FindAnyObjectByType<NavigationManager>();
        StartCoroutine(Instruction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Instruction()
    {
        //begin dialogue for context
        Debug.Log("Watch out for them cunts that will be busting through door.");

        yield return new WaitForSeconds(1f);

        Debug.Log("Maybe one of these tiny supes can be of help.");

        yield return new WaitForSeconds(2f);
        //Invoke the enemy event
        npcs.Movement();
    }
}
