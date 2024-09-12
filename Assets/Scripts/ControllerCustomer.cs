using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerCustomer : MonoBehaviour
{
    private GameManager _gameManager;
    public Button customerButton;
    private bool gameEnded = false;
    public Animator transition;
    public float transitionTime = 1f;
    public static float waitTime = 5f;

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
        StartCoroutine(PlayGame(waitTime));
    }

    //checking if the customer was clicked
    public void OnCustomerClicked()
    {
        if (!gameEnded)
        {
            Debug.Log("success");
            gameEnded = true;
            StopAllCoroutines(); 
            StartCoroutine(LoadLevel()); 
        }
    }

    IEnumerator PlayGame(float currentWaitTime)
    {
        yield return new WaitForSeconds(currentWaitTime);

        if (!gameEnded)
        {
            Debug.Log("lose health");
            GameManager.health--; 
            gameEnded = true;
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        GameManager.score += 10; 
        yield return new WaitForSeconds(transitionTime);
        waitTime = Mathf.Max(waitTime - 1f, 1f);

        SceneManager.LoadScene(2); 
    }
}
