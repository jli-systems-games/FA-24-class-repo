using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CritterController : MonoBehaviour
{
    public GameObject[] critters;  // Array to hold the critter objects
    private GameObject correctCritter;  // The correct critter to catch
    public float moveSpeed = 2f;  // Movement speed for the critters
    public Vector2 movementBounds;  // Define the area within which the critters can move

    public TextMeshProUGUI timerText;
    public float timerDuration = 15f;  // Set the timer duration
    private float timer;  // Countdown timer

    public TextMeshProUGUI messageText; // Reference to the message text component
    public float messageDisplayTime = 2.5f; // Time to display the result message

    private Vector2[] movementTargets;  // Targets for each critter to move toward

    void Start()
    {
        timer = timerDuration;  // Initialize the timer
        movementTargets = new Vector2[critters.Length]; // Initialize movement targets
        SetCorrectCritter();
        AssignRandomMovementTargets(); // Set initial movement targets
    }

    void Update()
    {
        MoveCritters(); // Move the critters around
        HandlePlayerClick(); // Check for player clicks
        UpdateTimer(); // Update the timer
    }

    // Randomly choose one critter as the correct one
    void SetCorrectCritter()
    {
        int correctIndex = Random.Range(0, critters.Length);
        correctCritter = critters[correctIndex];
        Debug.Log("Correct Critter is: " + correctCritter.name);
    }

    // Handle player clicks
    void HandlePlayerClick()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the Z coordinate is zero for 2D

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedCritter = hit.collider.gameObject;

                if (clickedCritter == correctCritter)
                {
                    Debug.Log("Correct critter clicked! You win!");
                    StartCoroutine(ShowResult("Correct Critter! You Win!", true));
                }
                else
                {
                    Debug.Log("Wrong critter clicked! You lose!");
                    StartCoroutine(ShowResult("Wrong Critter! You Lose!", false));
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }
        }
    }

    // Move the critters toward their assigned movement targets
    void MoveCritters()
    {
        for (int i = 0; i < critters.Length; i++)
        {
            // Move towards the target position
            critters[i].transform.position = Vector2.MoveTowards(critters[i].transform.position, movementTargets[i], moveSpeed * Time.deltaTime);

            // If the critter reaches the target, assign a new random target
            if (Vector2.Distance(critters[i].transform.position, movementTargets[i]) < 0.1f)
            {
                movementTargets[i] = GetRandomPositionWithinBounds();
            }
        }
    }

    // Assign random initial movement targets for each critter
    void AssignRandomMovementTargets()
    {
        for (int i = 0; i < critters.Length; i++)
        {
            movementTargets[i] = GetRandomPositionWithinBounds();
        }
    }

    // Get a random position within the movement bounds
    Vector2 GetRandomPositionWithinBounds()
    {
        float randomX = Random.Range(-movementBounds.x, movementBounds.x);
        float randomY = Random.Range(-movementBounds.y, movementBounds.y);
        return new Vector2(randomX, randomY);
    }

    // Update and display the timer
    void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Decrease the timer
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
        }
        else
        {
            Debug.Log("Time's up! You lose!");
            StartCoroutine(ShowResult("Time's Up! You Lose!", false));  // Player loses the mini-game
        }
    }

    // Show result message and then end the mini-game after a delay
    IEnumerator ShowResult(string message, bool didPlayerWin)
    {
        // Display the result message
        messageText.text = message;

        // Wait for the specified amount of time
        yield return new WaitForSeconds(messageDisplayTime);

        // End the current mini-game
        GameManager.instance.EndCurrentMiniGame(didPlayerWin);
    }
}
