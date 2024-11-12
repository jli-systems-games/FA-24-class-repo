using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelState {
    Start,End,Defeat
}

public class GameManager : MonoBehaviour
{
    LevelState currState;
    public static List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        EventManager.killedOff += ChangeLevelState;
        currState = LevelState.Start;
        GameObject[] ens = Array.ConvertAll<EnemyAttack,GameObject>( FindObjectsByType<EnemyAttack>(FindObjectsSortMode.None), (item) => item.gameObject);
        enemies = ens.OfType<GameObject>().ToList();

    }
    private void OnSceneUnloaded(Scene current)
    {
        Debug.Log("OnSceneUnloaded: " + current.name);
        ChangeLevelState(LevelState.Start);
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            Debug.Log("Quitting Scene1");
            ChangeScene();
        }
    }
    void ChangeLevelState(LevelState state)
    {
        currState = state;
        
        switch (currState)
        {
            case LevelState.Start:
                //grab all the enemies in the scene
                GameObject[] ens = Array.ConvertAll<EnemyAttack, GameObject>(FindObjectsByType<EnemyAttack>(FindObjectsSortMode.None), (item) => item.gameObject);
                enemies = ens.OfType<GameObject>().ToList();
                Debug.Log("yyee");
                break;
            case LevelState.End:
                //showing passing achievement
                Debug.Log("yay you killed them");
                break;
            case LevelState.Defeat:
                Debug.Log("Aw you lost");
                break;
        }
    }
    void ChangeScene()
    {
        //clear the static list of enemies
        if(enemies.Count > 0) { enemies.Clear(); }



        SceneManager.LoadScene("Level2");
    }
}
