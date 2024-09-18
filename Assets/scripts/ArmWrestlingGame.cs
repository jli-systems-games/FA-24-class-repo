using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ArmWrestlingGame : MonoBehaviour
{
    public Transform gameObjToRotate; 
    public float rotationAmount = 10f; //10 or 5,
    public float timeLimit = 30f; // round time
    public TMP_Text roundText; 
    public TMP_Text countdownText; 
    public TMP_Text timerText; 
    public TMP_Text winnerText;

    public AudioClip startSound; // sound
    private AudioSource audioSource; 

    private float countdown = 3f; 
    private float timer; 
    private bool isGameActive = false; 
    private int redScore = 0; 
    private int blueScore = 0; 
    private int round = 1; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartCountdown()); // start the timer
        timer = timeLimit;
    }

    void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString(); // remain time

            // player input
            if (Input.GetKeyDown(KeyCode.K))
            {
                gameObjToRotate.Rotate(Vector3.right * rotationAmount);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                gameObjToRotate.Rotate(Vector3.left * rotationAmount);
            }

            // checking the rotation
            float rotationX = gameObjToRotate.rotation.eulerAngles.x;

            
            if (rotationX > 180) rotationX -= 360;

            // 60 or -60
            if (rotationX >= 60 && isGameActive)
            {
                PlayerRedWins(); 
                EndRound(); 
            }
            else if (rotationX <= -60 && isGameActive)
            {
                PlayerBlueWins(); 
                EndRound(); 
            }

            // check the timer
            if (timer <= 0 && isGameActive)
            {
                EndGame(); 
            }
        }
    }




    IEnumerator StartCountdown()
    {
        countdown = 3f; 

        while (countdown > 0)
        {
            countdownText.text = Mathf.Ceil(countdown).ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "Start!";
        if (startSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(startSound); // play sound
        }
        isGameActive = true; 
        yield return new WaitForSeconds(1f);
        countdownText.text = ""; 
    }


    IEnumerator PrepareNextRound()
    {
        yield return new WaitForSeconds(1f); //add a little time

        round++; // add round
        roundText.text = "Round " + round; 

        
        winnerText.text = ""; // clear the information of winner

        // restart on the next round
        gameObjToRotate.rotation = Quaternion.identity; // restart the rotation
        timer = timeLimit; // timer
        StartCoroutine(StartCountdown()); // next round countdown
    }



    void EndGame()
    {
        isGameActive = false;

        float rotationX = gameObjToRotate.rotation.eulerAngles.x;

        
        if (rotationX > 180) rotationX -= 360;

        
        if (rotationX > 0)
        {
            PlayerRedWins(); // red
        }
        else if (rotationX < 0)
        {
            PlayerBlueWins(); // blue
        }

        EndRound();
    }

    void EndRound()
    {
        isGameActive = false; 

        
        if (redScore == 2 || blueScore == 2)
        {
            winnerText.text = (redScore == 2) ? "red win!" : "blue win!";
            
        }
        else
        {
            StartCoroutine(PrepareNextRound()); 
        }
    }






    public void PlayerRedWins()
    {
        winnerText.text = "red win";
        redScore++;
        CheckMatchWinner();
    }

    public void PlayerBlueWins()
    {
        winnerText.text = "blue win!";
        blueScore++;
        CheckMatchWinner();
    }

    private void CheckMatchWinner()
    {
        if (redScore == 2)
        {
            winnerText.text = "red wins the game!";
            SceneManager.LoadScene("redwin"); // switch scene to redwin
        }
        else if (blueScore == 2)
        {
            winnerText.text = "blue wins the game!";
            SceneManager.LoadScene("bluewin"); // switch scene to bluewin
        }
    }

}
