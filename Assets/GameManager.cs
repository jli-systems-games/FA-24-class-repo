using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("PLAYER 1")]
    public GameObject player1;
    public GameObject p1guns;
    public List<GameObject> p1_guns = new();
    public GameObject parent1;

    [Header("PLAYER 2")]
    public GameObject player2;
    public GameObject p2guns;
    public List<GameObject> p2_guns = new();
    public GameObject parent2;



    [Header("CAMERA")]
    public GameObject cam;
    public List<GameObject> camLocations = new();
    public float moveSpeed = 10f;

    [Header("MENU")]
    public GameObject MenuStuff;

    [Header("START")]
    public GameObject Sliders;

    [Header("CANVAS")]
    public GameObject Canvas_PickOne;
    public GameObject Canvas_PickTwo;

    [Header("OTHER")]
    public TextMeshProUGUI displayText;
    public GameObject Text;
    private bool isMovingToPlayerOne = false;
    private bool isMovingToPlayerTwo = false;
    private bool isMovingToMiddle = false;



    // Start is called before the first frame update
    void Start()
    {
        Menu();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToPlayerOne)
        {
            // Move the camera smoothly to the target location
            cam.transform.position = Vector3.Lerp(cam.transform.position, camLocations[1].transform.position, moveSpeed * Time.deltaTime);

            // Stop moving once the camera is close enough to the target position
            if (Vector3.Distance(cam.transform.position, camLocations[1].transform.position) < 0.1f)
            {
                cam.transform.position = camLocations[1].transform.position; // Snap to the final position
                isMovingToPlayerOne = false; // Stop moving
            }
        }

        if (isMovingToPlayerTwo)
        {
            // Move the camera smoothly to the target location
            cam.transform.position = Vector3.Lerp(cam.transform.position, camLocations[2].transform.position, moveSpeed * Time.deltaTime);

            // Stop moving once the camera is close enough to the target position
            if (Vector3.Distance(cam.transform.position, camLocations[2].transform.position) < 0.1f)
            {
                cam.transform.position = camLocations[2].transform.position; // Snap to the final position
                isMovingToPlayerTwo = false; // Stop moving
            }
        }

        if (isMovingToMiddle)
        {
            // Move the camera smoothly to the target location
            cam.transform.position = Vector3.Lerp(cam.transform.position, camLocations[0].transform.position, moveSpeed * Time.deltaTime);

            // Stop moving once the camera is close enough to the target position
            if (Vector3.Distance(cam.transform.position, camLocations[0].transform.position) < 0.1f)
            {
                cam.transform.position = camLocations[0].transform.position; // Snap to the final position
                isMovingToMiddle = false; // Stop moving
            }
        }
    }

    public void Menu()
    {
        // only start screen active

        MenuStuff.SetActive(true);

        player1.SetActive(false);
        player2.SetActive(false);



        foreach (GameObject item in p1_guns)
        {
            item.SetActive(false);
        }

        foreach (GameObject item in p2_guns)
        {
            item.SetActive(false);
        }

        Canvas_PickOne.SetActive(false);
        Canvas_PickTwo.SetActive(false);

        Sliders.SetActive(false);
    }

    public void PlayerOne()
    {
        MenuStuff.SetActive(false);

        isMovingToPlayerOne = true; // Start camera movement
        isMovingToPlayerTwo = false;
        isMovingToMiddle = false;

        player1.SetActive(true);
        player2.SetActive(false);

        Canvas_PickOne.SetActive(true);
        Canvas_PickTwo.SetActive(false);

        //move to play, move to p1
        // turn on player one in middle
        //reroll button
    }

    public void PlayerTwo()
    {

        isMovingToPlayerTwo = true; // Start camera movement
        isMovingToPlayerOne = false; // Start camera movement
        isMovingToMiddle = false;

        player1.SetActive(false);
        player2.SetActive(true);

        Canvas_PickOne.SetActive(false);
        Canvas_PickTwo.SetActive(true);
        //move to p2
        // turn on player two in middle
        //turn off p1
    }

    public void StartGame()
    {
        isMovingToPlayerOne = false; // Start camera movement
        isMovingToPlayerTwo = false;
        isMovingToMiddle = true;

        //move players to location
        player1.SetActive(true);
        player2.SetActive(true);

        player1.GetComponent<PlayerOne>().PlayPositions();
        player2.GetComponent<PlayerOne>().PlayPositions();


        Canvas_PickOne.SetActive(false);
        Canvas_PickTwo.SetActive(false);


        //turn on bullet


        //tun on movement

        p1guns.SetActive(false);
        p2guns.SetActive(false);

        Sliders.SetActive(true);
        StartCoroutine(BeginTimer());
    }

    public void EndScreen()
    {
        //turn on winner text
        //see which player is still active and change text color
        //play again button
    }

    IEnumerator BeginTimer()
    {
        int countdown = 3; // 3-second countdown

        while (countdown > 0)
        {
            displayText.text = countdown.ToString(); // Update text with the countdown number
            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdown--; // Decrease the countdown
        }

        p1guns.SetActive(true);
        p2guns.SetActive(true);

        parent1.GetComponent<PlayerMovement>().enabled = true;
        parent2.GetComponent<PlayerMovement>().enabled = true;

        Text.SetActive(false);
        //displayText.text = "Time's Up!"; // Display "Time's Up!" when the countdown ends
    }
}
