using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryDrop : MonoBehaviour
{

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState) 
    {

    }

    public void StartMicroGame(int score)
    {

    }
}
