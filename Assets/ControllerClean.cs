using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerClean : MonoBehaviour
{
    private GameManager _gameManager;
    public List<GameObject> dirtySpots;
    private int spotsCleared;
    private bool gameEnded;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GameManager>();
        spotsCleared = 0;
        StartMicroGame(GameManager.health);
    }

    public void StartMicroGame(int currentHealth)
    {
        gameEnded = false;
        StartCoroutine(PlayGame(10f));
    }

    //check mouse and if player drags on spot then clean it
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("DirtySpot"))
                {
                    CleanSpot(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Cleaned Spot");
                }
            }
        }
    }

    //removing the spot from the list and checking they're all removed
    public void CleanSpot(GameObject spot)
    {
        dirtySpots.Remove(spot);

        if (dirtySpots.Count == 0)
        {
            Debug.Log("Cleaned All Spots");
            GameManager.score += 10;
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
