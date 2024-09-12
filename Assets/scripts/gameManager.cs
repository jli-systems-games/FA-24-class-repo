using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public int clearedGames;
    public GameObject startCanvas;
    public GameObject transitionCanvas;
    public string[] games;

    public GameObject timer;
    public GameObject timerGame2;
    public timer timerr;
    public timerGame2 timerrr;

    bool isDone = false;

    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<gameManager>().Length; i++)
        {
            if(Object.FindObjectsOfType<gameManager>()[i] != this)
            {
                if(Object.FindObjectsOfType<gameManager>()[i].name == gameObject.name)
                {
                Destroy(gameObject);
                }
            }
        }

        
        DontDestroyOnLoad(this.gameObject);
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string sceneName in games)
        {
            if (scene.name == sceneName)
            {
                clearedGames++;
            }
        }
        
    }

    public void StartGame()
    {
        transitionCanvas.gameObject.SetActive(true);
        startCanvas.gameObject.SetActive(false);
    }

    IEnumerator WaitToMakeTrue()
    {
        yield return new WaitForSeconds(8);
        isDone = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("game2"))
        {
            GameObject timerGame2 = GameObject.FindWithTag("timer");
            timerrr = timerGame2.GetComponent<timerGame2>();

            if ( clearedGames % 4 == 0)
            {
                if (!isDone)
                {
                    timerrr.remainingTime -= 2;
                    Debug.Log("speed up");
                    isDone = true;
                    StartCoroutine(WaitToMakeTrue());
                }
                
            }
        
        }
        else
        {
            GameObject timer = GameObject.FindWithTag("timer");
            timerr = timer.GetComponent<timer>();
            if (clearedGames % 4 == 0)
            {
                if (!isDone)
                {
                    timerr.remainingTime -= 2;
                    Debug.Log("speed up");
                    isDone = true;
                    StartCoroutine(WaitToMakeTrue());
                }
            }
        
        }


    }

        
}

