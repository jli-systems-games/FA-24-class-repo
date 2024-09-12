using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerMatch : MonoBehaviour
{
    private GameManager _gameManager;
    public List<Sprite> pastrySprites;
    public Image displayedPastry;
    public List<Button> pastryButtons;
    private Sprite correctPastry; 
    private bool gameEnded;
    public Animator transition;
    public float transitionTime = 1f;
    public static float waitTime = 10f;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartMicroGame(GameManager.health);
    }

    //pick a random pastry
    public void StartMicroGame(int currentHealth)
    {
        int randomIndex = Random.Range(0, pastrySprites.Count);
        correctPastry = pastrySprites[randomIndex];

        displayedPastry.sprite = correctPastry;

        AssignPastriesToButtons();
        
        gameEnded = false;
        StartCoroutine(PlayGame(waitTime));
    }

    //assign pastries to the buttons and add click listeners
    void AssignPastriesToButtons()
    {
        for (int i = 0; i < pastryButtons.Count; i++)
        {
            pastryButtons[i].GetComponent<Image>().sprite = pastrySprites[i];
        }

        for (int i = 0; i < pastryButtons.Count; i++)
        {
            int index = i;
            pastryButtons[i].onClick.RemoveAllListeners();
            pastryButtons[i].onClick.AddListener(() => CheckPlayerChoice(pastryButtons[index]));
        }
    }

    //check if player clicked the right pastry
    void CheckPlayerChoice(Button chosenButton)
    {
        if (chosenButton.GetComponent<Image>().sprite == correctPastry)
        {
            Debug.Log("Correct choice");
            GameManager.score += 10; 
            gameEnded = true;
            StartCoroutine(LoadLevel());           
        }
        else
        {
            Debug.Log("Game Over");
            GameManager.health--;
            gameEnded = true;
            StartCoroutine(LoadLevel());       
        }
    }

    IEnumerator PlayGame(float currentWaitTime)
    {
        yield return new WaitForSeconds(currentWaitTime);

        if (!gameEnded)
        {
            Debug.Log("Game Over");
            GameManager.health--; 
            gameEnded = true;
            StartCoroutine(LoadLevel());       
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        waitTime = Mathf.Max(waitTime - 1f, 1f);

        SceneManager.LoadScene(3); 
    }
}
