using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TMP_Text gameOverText; // TextMeshPro component for "Game Over" message
    public TMP_Text messageText; // TextMeshPro component for "You lost some mini-games" message

    void Start()
    {
        int lossCount = GameManager.instance.GetLossCount();

        if (lossCount > 0)
        {
            // Set the messages for the lost state
            gameOverText.text = "Game Over!";
            messageText.text = "You lost " + lossCount + " mini-games.";
        }
        else
        {
            // Customize text for winning all mini-games if needed
            gameOverText.text = "Congratulations!";
            messageText.text = "You won all mini-games!";
        }
    }

    public void RetryButton()
    {
        // Reset game manager and go back to the main menu
        GameManager.instance.ResetMiniGameSequence();
        SceneManager.LoadScene("Main Menu"); // Load the Main Menu scene
    }
}
