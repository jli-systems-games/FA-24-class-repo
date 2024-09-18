using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.Windows;

public enum GameState
{
    StartScreen, Instruction, EndScreen
}
public class GameManager : MonoBehaviour
{
    public GameState state;
    public bool gameStart,startAudio;
    public GameObject _start, _instruct, _end, _canvas;


    // Start is called before the first frame update
    void Start()
    {
        state = GameState.StartScreen;  
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.Instruction)
        {
            if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                Debug.Log("start");
                StartGame();

            }
        }
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.StartScreen:
                gameStart = false;

                break;
            case GameState.Instruction:

                break;
            case GameState.EndScreen:
                _canvas.SetActive(true);
                _instruct.SetActive(false);
                _end.SetActive(true);

                break;
        }
    }

    public void InstructionON()
    {
        _start.SetActive(false);
        _instruct.SetActive(true);
        ChangeState(GameState.Instruction);
    }

    void StartGame()
    {
        _canvas.SetActive(false);
        gameStart = true;
        startAudio = true;
    }
}
