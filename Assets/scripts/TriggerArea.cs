using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerArea : MonoBehaviour
{
    public GameObject UICanvas; 
    public GameObject winText;  
    public GameObject loseText;  
    //public GameObject Restart;  
    private float countdownTime = 15f; 
    private bool gameEnded = false; 
    private TextMeshProUGUI timerText; 

    private float timeRemaining;

    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        
        timerText = UICanvas.GetComponentInChildren<TextMeshProUGUI>();
        
        timeRemaining = countdownTime;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            timerText.text = $"{(int)(timeRemaining / 60)}:{(int)(timeRemaining % 60):00}";
            timeRemaining -= Time.deltaTime;
            yield return null; 
        }

        
        if (!gameEnded)
        {
            
            CheckCollisions(false); 
        }
    }

    private void CheckCollisions(bool collisionDetected)
    {
        if (collisionDetected)
        {
           
            loseText.SetActive(true);
        }
        else
        {
            winText.SetActive(true);
        }

        gameEnded = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timeRemaining <= 0)
        {
            Debug.Log("Is collide");
            CheckCollisions(true);
        }
    }

    private void Update()
    {
        if (gameEnded)
        {
            return; 
        }
    }
}
