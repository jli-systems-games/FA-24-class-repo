using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTransition : MonoBehaviour
{
    public TMP_Text transitionMessage;
    public TMP_Text countdownText;
    private float countdownTime = 2.5f; // 2.5-second countdown

   

    void Start()
    {
        transitionMessage.text = "Get Ready to Scuttle!";
        StartCoroutine(CountdownToNextGame());
    }

    IEnumerator CountdownToNextGame()
    {
      

        // Wait for the countdown to complete
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }


        Debug.Log("Doors closed. Loading next game.");

        LoadNextGame(); // Load the next mini-game scene
    }

    void LoadNextGame()
    {
        string nextGameScene = GameManager.instance.GetNextMiniGame();
        SceneManager.LoadScene(nextGameScene);
    }
}
