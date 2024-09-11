using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TMP_Text gameOverText; // TextMeshPro component for "Game Over" message
    public TMP_Text lostMessageText; // TextMeshPro component for "You lost some mini-games" message

    void Start()
    {
        if (GameManager.instance.HasLostAnyMiniGame())
        {
            // Set the messages for the lost state
            gameOverText.text = "Game Over!";
            lostMessageText.text = "You lost some mini-games.";

        }
        else
        {
            // Customize text for winning all mini-games if needed
            gameOverText.text = "Congratulations!";
            lostMessageText.text = "You won all mini-games!";

        }
    }
}
