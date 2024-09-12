using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{

    // private int currentSceneIndex = 0;
    public Animator transition;
    public float transitionTime = 1f;

    public void PlayGame()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        GameManager.health = 3;
        GameManager.score = 0; 

        SceneManager.LoadScene(1); 

    }
}
