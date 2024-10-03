using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public enum GameState
 {
        EndScreen, Level1, Level2, Death,
 }
public class GameManager : MonoBehaviour
{
    public NavigationManager[] npcs;
    public TMP_Text context;
    public GameObject Panelparent,Success,dead, plyr;
    public string[] lines;
    public GameState state;

    void Start()
    {
        
        AssignState(GameState.Level1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
    public void AssignState(GameState newState)
    {
        state = newState;

        switch (state) 
        {
            case GameState.EndScreen:
                //disable enemies and players
                Ending();
                //pull up endscreen ui;
                break;
            case GameState.Level1:
                StartCoroutine(Instruction());
                break;
            case GameState.Level2:
                //Coroutine for context and find new enemy objects;
                StartCoroutine(SecondStageIntro());
                break;
            case GameState.Death:
                //provide restart button;
                Death();
                break;

        }
    }
    IEnumerator Instruction()
    {
        //begin dialogue for context
        context.text = lines[0];

        yield return new WaitForSeconds(3f);

        context.text = lines[1];

        yield return new WaitForSeconds(5f);
        //Invoke the enemy event
        Panelparent.SetActive(false);
        npcs[0].Movement();
    }

    IEnumerator SecondStageIntro()
    {
        Panelparent.SetActive(true);

        context.text = lines[2];
        yield return new WaitForSeconds(2f);

        Panelparent.SetActive(false);
        //invoke 2nd level enemies
        npcs[1].Movement();
        yield return new WaitForSeconds(3f);
        npcs[2].Movement();
    }

    void Ending()
    {
        foreach (var npc in npcs)
        {
            npc.gameObject.SetActive(false);
        }
        Success.SetActive(true);
    }
    void Death()
    {
        dead.SetActive(true);
        plyr.SetActive(false);
    }

    public void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
