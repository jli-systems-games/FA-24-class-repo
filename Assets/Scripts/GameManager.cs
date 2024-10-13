using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState 
{ 
    beginning, winter, rotting, summer, end,
}

public class GameManager : MonoBehaviour
{
    public GameState current;
    [SerializeField] GameObject rotScreen, plyr, endscrn;
    Dictionary<GameObject, bool> jars = new Dictionary<GameObject, bool>();
    bool success;
    void Start()
    {
        current = GameState.summer;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (jars != null)
        {
            checkJarStatus();
        }
        if (success)
        {
            ChangeState(GameState.end);
            success = false;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Reset();
        }
    }

    public void ChangeState(GameState state)
    {
        current = state;

        switch (current)
        {
            case GameState.summer:
                //allow 
                
                break;
            case GameState.winter:
                break;
            case GameState.rotting:
                //if objects get reseted with parent and it is summer.
                //turn on fail scene.
                rotScreen.SetActive(true);
                plyr.SetActive(false);
                break;
            case GameState.end:
                endscrn.SetActive(true);
                break;
        }
    }

    public void addJars(GameObject jar)
    {
        if (!jars.ContainsKey(jar))
        {
            jars.Add(jar, false);
            Debug.Log("have added jar");
        }
    }

    public void Updatejars(GameObject jar)
    {
        
        
        if (jars.ContainsKey(jar))
        {
            jars[jar] = true; // Mark it 
        }
       
    }

    public void checkJarStatus()
    {
        //Debug.Log("Checking");

        int count = 0;
        if (!success)
        {
            foreach (var status in jars)
            {
                bool correct = status.Value;
                 GameObject ky = status.Key;
                //Debug.Log(ky.name + correct);
            if (!correct)
            {
                return;
            }
            else
            {
                count++;
                if(count >= 2)
                {   
                    success = true;
                    break;
                }
            }
        }
        
        }
       
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
