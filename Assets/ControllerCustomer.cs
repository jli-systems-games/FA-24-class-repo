using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerCustomer : MonoBehaviour
{
    private GameManager _gameManager;
    public Button customerButton;
    private bool gameEnded = false;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GameManager>();

        customerButton.onClick.AddListener(OnCustomerClicked);

        StartMicroGame(GameManager.health);
    }

    public void StartMicroGame(int currentHealth)
    {
        gameEnded = false;
        StartCoroutine(PlayGame(5f));
    }

    //checking if the customer was clicked
    public void OnCustomerClicked()
    {
        if (!gameEnded)
        {
            Debug.Log("success");
            GameManager.score += 10; 
            gameEnded = true;
            StopAllCoroutines(); 
            _gameManager.ChangeState(GameState.Transition); 
        }
    }

    IEnumerator PlayGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!gameEnded)
        {
            Debug.Log("game over");
            GameManager.health--; 
            gameEnded = true;
            _gameManager.ChangeState(GameState.Transition);
        }
    }
}
