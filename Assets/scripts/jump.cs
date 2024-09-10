using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerJump : MonoBehaviour
{
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManagerManager = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GameManager>();

        StartCoroutine(PlayGame(5f));
    }

    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds();
        gameManager.Change
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {

    }

    public void StartMicroGame()
    {

    }
}
