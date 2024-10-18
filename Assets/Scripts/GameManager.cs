using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public enum GameState {
    begin, Hunger, Fetch, Irritable, Game, End
}

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemyStates enemyStates;
    [SerializeField] PlayerMovement plyr;
    [SerializeField] GameObject hBar, iBar, _instructText, button, endscreen, cracks;
    [SerializeField] TMP_Text instruct, keys;
    [SerializeField] AudioSource cracking;
    public static GameState currentState;
    bool ended, knowHunger, knowFetch, knowAttack;
    void Start()
    {
        currentState = GameState.begin;
        StartCoroutine(_begin());
    }

    // Update is called once per frame
    void Update()
    {
        if (ended)
        {/*
            if(Input.GetKeyUp(KeyCode.Return))
            {
                Reset(); 
            }*/
        }
    }
    public void ChangeGState(GameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case GameState.Hunger:
                // Debug.Log("go feeding the beast");
                instruct.text = "Go feed the beast";
                button.SetActive(true);

                plyr.tuH = false;
                break;
            case GameState.Fetch:
                // Debug.Log("Entertain it");
                if (!knowFetch)
                {
                    StartCoroutine(fetchInstruct());
                    plyr.tuF = false;
                    knowFetch = true;
                }
              
                break;
            case GameState.Irritable:
                //disable plyr turn and throw controll;
                //Debug.Log("fight");
                if (!knowAttack)
                {
                    StartCoroutine(attacking());
                    knowAttack = true;
                }
                
                break;
            case GameState.Game:
                // Debug.Log("Gamingg");
                break;
            case GameState.End:
                endscreen.SetActive(true);
                plyr.enabled = false;
                ended = true;
                break;
        }
    }
    IEnumerator _begin()
    {
        //Debug.Log("shatter glass");
        _instructText.SetActive(true);
        instruct.text = "Welcome! Here at Cryptid ResearchTM we like to keep our specimen content and fed";
        yield return new WaitForSeconds(2f);

        plyr.viewStats();
        instruct.text = "You see them?";

        yield return new WaitForSeconds(4f);
        instruct.text = "Don't let those fill up";

        yield return new WaitForSeconds(2f);
        plyr.resetCam();

        yield return new WaitForSeconds(2f);
        ChangeGState(GameState.Irritable);
    }
    IEnumerator fetchInstruct()
    {
        instruct.text = "Isn't it lovely? He is less hungry now";
        yield return new WaitForSeconds(2.5f);

        instruct.text = "Yikes the boredom bar seems to be a bit high";

        yield return new WaitForSeconds(2.5f);

        instruct.text = "You should entertain it";
        keys.text = "E";

        yield return new WaitForSeconds(2f);

        _instructText.SetActive(false);

    }
    IEnumerator attacking()
    {
        
        //play snap sound;
        yield return new WaitForSeconds(1.5f);

        plyr.tuR = false;
        eventManager.countChicks(hBar, iBar, "increase");


        yield return new WaitForSeconds(1f);

        cracks.SetActive(true);
        cracking.Play();
        instruct.text = "ah";

        yield return new WaitForSeconds(1.5f);
        instruct.text = "You should try breakout of this.";

       

        //_instructText.SetActive(false);
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
