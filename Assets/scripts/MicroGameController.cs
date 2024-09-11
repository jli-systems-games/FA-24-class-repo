using System.Collections;
using UnityEngine;

public class MicroGameController : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;

        if (_gameManager == null)
        {
            Debug.LogError("GameManager not found. Make sure it exists in the TransitionScene.");
        }
        //_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    public void StartMicroGame()
    {
        Debug.Log("Microgame started!");
        StartCoroutine(EndMicroGame());
    }


    IEnumerator EndMicroGame()
    {
        yield return new WaitForSeconds(10f);
        _gameManager.ChangeState(GameState.Transition);
    }
}
