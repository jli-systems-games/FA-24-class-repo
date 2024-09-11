using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryMatchManager : MonoBehaviour
{
    public GameObject[] cards;        // Array of card objects
    public Sprite cardBack;           // Placeholder for card's back side
    public Sprite[] cardFaces;        // Placeholder for card's face side (bug critter images later)
    public TextMeshProUGUI timerText; // TextMeshPro for displaying the timer
    public TextMeshProUGUI messageText; // Reference to the message text component
    public float messageDisplayTime = 3.5f; // Time to display the result message

    private bool canFlip = true;      // Controls if cards can be flipped
    private GameObject firstCard;     // Stores the first flipped card
    private GameObject secondCard;    // Stores the second flipped card
    private int totalMatches;         // Tracks how many matches have been found
    private int currentMatches = 0;   // Tracks player progress
    private float timeRemaining = 15f;  // 15-second timer

    void Start()
    {
        ShuffleCards();
        ResetCards();
        totalMatches = cards.Length / 2; // Total pairs
    }

    void Update()
    {
        UpdateTimer();
    }

    // Shuffle card positions
    void ShuffleCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            int randIndex = Random.Range(0, cards.Length);
            Vector3 tempPosition = cards[i].transform.position;
            cards[i].transform.position = cards[randIndex].transform.position;
            cards[randIndex].transform.position = tempPosition;
        }
    }

    // Resets all cards to be face down at the start
    void ResetCards()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<Image>().sprite = cardBack; // Placeholder image for card back
        }
    }

    // Called when a card is clicked
    public void OnCardClicked(GameObject clickedCard)
    {
        if (!canFlip || clickedCard == firstCard) return; // Prevent double click

        clickedCard.GetComponent<Image>().sprite = cardFaces[clickedCard.GetComponent<Card>().cardID]; // Reveal card face

        if (firstCard == null)
        {
            firstCard = clickedCard; // First card clicked
        }
        else
        {
            secondCard = clickedCard; // Second card clicked
            StartCoroutine(CheckForMatch());
        }
    }

    // Check if the two flipped cards match
    IEnumerator CheckForMatch()
    {
        canFlip = false; // Disable flipping while checking

        yield return new WaitForSeconds(1f); // Wait before comparing cards

        if (firstCard.GetComponent<Card>().cardID == secondCard.GetComponent<Card>().cardID)
        {
            Debug.Log("Match found!");
            firstCard.GetComponent<Button>().interactable = false; // Disable buttons for matched cards
            secondCard.GetComponent<Button>().interactable = false;
            currentMatches++;

            if (currentMatches == totalMatches)
            {
                Debug.Log("You matched all pairs!");
                GameManager.instance.EndCurrentMiniGame(true); // End mini-game with a win
            }
        }
        else
        {
            // No match, flip cards back
            firstCard.GetComponent<Image>().sprite = cardBack;
            secondCard.GetComponent<Image>().sprite = cardBack;
            Debug.Log("No match!");
        }

        firstCard = null;
        secondCard = null;
        canFlip = true; // Allow further card flipping
    }

    // Update the timer every frame
    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.Floor(timeRemaining).ToString(); // Update timer display
        }
        else
        {
            // Ensure the timeRemaining doesn't go below zero
            timeRemaining = 0;
            timerText.text = "0"; // Display zero when time is up

            // End the mini-game if time runs out and hasn't been handled yet
            StartCoroutine(ShowResult("Time's Up! You Lose!"));
        }
    }

    // Show result message and then end the mini-game after a delay
    IEnumerator ShowResult(string message)
    {
        // Display the result message
        messageText.text = message;

        // Wait for the specified amount of time
        yield return new WaitForSeconds(messageDisplayTime);

        // End the current mini-game
        GameManager.instance.EndCurrentMiniGame(message == "You Matched All Pairs!"); // End game with win or loss
    }
}
