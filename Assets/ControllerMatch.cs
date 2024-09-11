using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMatch : MonoBehaviour
{
    private GameManager _gameManager;
    public List<Sprite> pastrySprites;
    public Image displayedPastry;
    public List<Button> pastryButtons;
    private Sprite correctPastry; 
    private bool gameEnded;

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
        StartCoroutine(PlayGame(10f));
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
            _gameManager.ChangeState(GameState.Transition);
        }
        else
        {
            Debug.Log("Game Over");
            GameManager.health--;
            gameEnded = true;
            _gameManager.ChangeState(GameState.Transition);
        }
    }

    IEnumerator PlayGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!gameEnded)
        {
            Debug.Log("Game Over");
            GameManager.health--; 
            gameEnded = true;
            _gameManager.ChangeState(GameState.Transition);
        }
    }
}
