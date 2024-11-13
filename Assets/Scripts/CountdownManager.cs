using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Reference to the countdown text
    public string battleSceneName = "BattleScene"; // Name of the battle scene
    public Button continueButton; // Reference to the continue button

    private void Start()
    {
        continueButton.onClick.AddListener(StartCountdown); // Assign the button listener
    }

    // Method to start the countdown
    public void StartCountdown()
    {
        continueButton.gameObject.SetActive(false); // Hide the continue button
        StartCoroutine(CountdownCoroutine());
    }

    // Coroutine for the countdown
    private IEnumerator CountdownCoroutine()
    {
        // Countdown sequence
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Display the countdown number
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }

        // Show "Get ready to fight!"
        countdownText.text = "Get ready to fight!";
        yield return new WaitForSeconds(1f); // Wait for another second

        // Load the battle scene
        SceneManager.LoadScene(battleSceneName);
    }
}
