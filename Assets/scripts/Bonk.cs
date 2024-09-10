using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBonk : MonoBehaviour
{

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GameManager>();

        StartCoroutine(PlayGame(5f));
    }

    // Update is called once per frame
    IEnumerator PlayGame(float waitTime)
    {
        //do before - in this case, the game
        yield return new WaitForSeconds(waitTime);
        _gameManager.ChangeState(GameState.Transition);
    }

    public void ChangeState(GameState newState)
    {

    }

    public void StartMicroGame()
    {

    }
}
