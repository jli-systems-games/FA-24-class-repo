using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameButton : MonoBehaviour
{
    // Call this method to simulate winning and transition to the next game
    public void OnWinButtonPressed()
    {
        GameManager.instance.EndCurrentMiniGame(true); // Pass 'true' for win
    }

    // Call this method to simulate losing and transition to the end screen
    public void OnLoseButtonPressed()
    {
        GameManager.instance.EndCurrentMiniGame(false); // Pass 'false' for loss
    }
}
