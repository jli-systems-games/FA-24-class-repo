using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public enum GameState
{
    StartScreen, Instruction, EndScreen, GamePlay
}
public class GameManager : MonoBehaviour
{
    public GameState state;
    public bool gameStart,startAudio;
    public GameObject _start, _instruct, _end, _canvas, _game, _background, restartButton;
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
                //Debug.Log("start");
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
            case GameState.GamePlay:
                _game.SetActive(true);
                _background.SetActive(false);
                break;
            case GameState.EndScreen:
                _background.SetActive(true);
                _game.SetActive(false);
                _end.SetActive(true);
                StartCoroutine(reStart());
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
        //_canvas.SetActive(false);
        gameStart = true;
        startAudio = true;
    }

    IEnumerator reStart()
    {
        yield return new WaitForSeconds(1f);
        restartButton.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
