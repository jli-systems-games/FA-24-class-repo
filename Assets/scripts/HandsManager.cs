using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    public List<GameObject> hands; 

    

    private List<HandsPop> handsPopComponents = new List<HandsPop>(); 
    private int clearedCount = 0;

    public GameObject win;
    private GameManager _gameManager;

    public int handNumber = 0;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        win.SetActive(false);

        
        foreach (GameObject handObject in hands)
        {
            Debug.Log("HandsAppear");
            HandsPop handsPop = handObject.GetComponent<HandsPop>();
            if (handsPop != null)
            {
                handsPopComponents.Add(handsPop);
                handObject.SetActive(false); 
            }
        
        }

        StartNextHand(); 
    }

    
    public void StartNextHand()
    {
        if (clearedCount >= hands.Count)
        {
            Debug.Log("HandsCleared!");
            //win.SetActive(true);
            _gameManager.ChangeState(GameState.Transition);
            return;
        }

        else
        {
            List<HandsPop> availableHands = new List<HandsPop>();
            foreach (HandsPop hand in handsPopComponents)
            {
                if (!hand.gameObject.activeSelf)
                {
                    availableHands.Add(hand);
                }
            }

            if (availableHands.Count > 0)
            {
                int randomIndex = Random.Range(0, availableHands.Count);
                HandsPop selectedHand = availableHands[randomIndex];
                selectedHand.gameObject.SetActive(true);
                selectedHand.Initialize(this);
                StartCoroutine(selectedHand.ShowHand());
            }
            else
            {
                Debug.Log("No available hands to activate.");
            }
        }
    }

   
    public void OnHandCleared()
    {
        clearedCount++;
        _gameManager.currentGameIndex++;
        //StartNextHand();
        //add code to call win state
        //once win state is active, have THAT call the change state
        StartCoroutine(ShowWin());

    }
    IEnumerator ShowWin()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _gameManager.ChangeState(GameState.Transition);
    }
}
