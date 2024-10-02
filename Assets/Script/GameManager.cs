using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    NavigationManager npcs;
    public TMP_Text context;
    public GameObject Panelparent;
    public string[] lines;
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
        context.text = lines[0];
        Debug.Log("Watch out for them cunts that will be busting through door.");

        yield return new WaitForSeconds(2f);

        context.text = lines[1];
        Debug.Log("Maybe one of these tiny supes can be of help.");

        yield return new WaitForSeconds(2f);
        //Invoke the enemy event
        Panelparent.SetActive(false);
        npcs.Movement();
    }
}
