using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
 public enum GameState
 {
        EndScreen, Level1, Level2, Death,
 }
public class GameManager : MonoBehaviour
{
    NavigationManager npcs;
    public TMP_Text context;
    public GameObject Panelparent;
    public string[] lines;
    public GameState state;

    void Start()
    {
        npcs = FindAnyObjectByType<NavigationManager>();
        AssignState(GameState.Level1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AssignState(GameState newState)
    {
        state = newState;

        switch (state) 
        {
            case GameState.EndScreen:
                //disable enemies and players
                //pull up endscreen ui;
                break;
            case GameState.Level1:
                StartCoroutine(Instruction());
                break;
            case GameState.Level2:
                //Coroutine for context and find new enemy objects;
                break;
            case GameState.Death:
                //provide restart button;

                break;

        }
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
