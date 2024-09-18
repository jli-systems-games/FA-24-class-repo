using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame()
    {
        // can start coroutines in one script to call the ienum in abother script tbu then if that object w the coroutine is destroyed that ienum can stil be called??

        while (player.transform.position.x > 10)
        {

        }

        yield break; //FIXES THAT ONETHING WHERE IT DOESNT LET U MAKE THE COROUTINE
        //MUST BE AT END

        //Scoreboard.Instance.Lose();
        //scoreboard being a separate script, since its only used once in this script we forego the thing at the beginning
        //"use wisely"??
    }
}
