using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LevelState {
    Preparing,Start,End,Defeat
}

public class GameManager : MonoBehaviour
{
    public static LevelState currState;
    public static List<GameObject> enemies = new List<GameObject>();
    public static List<ToolObstacle> tools = new List<ToolObstacle>();
    [SerializeField] GameObject nextButton, Failure;
    //[SerializeField] EventManager _event;
    List<Level> levels = new List<Level> { new Level(4, 2), new Level(2)};
    string[] sceneNames = {"SampleScene","Level2" };
    int index = 0;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        EventManager.killedOff += ChangeLevelState;

        ChangeLevelState(LevelState.Preparing);
        


    }
    private void OnSceneUnloaded(Scene current)
    {
        Debug.Log("OnSceneUnloaded: " + current.name);
        ChangeLevelState(LevelState.Preparing);
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {

            beginLevel();
        }
    }
    void ChangeLevelState(LevelState state)
    {
        currState = state;
        
        switch (currState)
        {
            case LevelState.Preparing:
                //show ui of the limits of the number of the tools allow to use;
                Debug.Log("preparing");
              StartCoroutine(setUp());
              nextButton.SetActive(false);

                break;
            case LevelState.Start:
                //grab all the enemies in the scene
                GameObject[] ens = Array.ConvertAll<EnemyAttack, GameObject>(FindObjectsByType<EnemyAttack>(FindObjectsSortMode.None), (item) => item.gameObject);
                enemies = ens.OfType<GameObject>().ToList();
                //disable the tool objects;
                if (tools.Count > 0)
                {
                    foreach (ToolObstacle t in tools)
                    {
                        t.gameObject.SetActive(false);
                    }
                }
                break;
            case LevelState.End:
                //showing passing achievement
                nextButton.SetActive(true);
                break;
            case LevelState.Defeat:
                Failure.SetActive(true);
                break;
        }
    }
    public void ChangeScene()
    {
        //clear the static list of enemies
        if(enemies.Count > 0) { enemies.Clear(); }
        //clear tools list 
        if(tools.Count > 0) { tools.Clear(); }
        index++;
        SceneManager.LoadScene(sceneNames[index]);
    }

    public void beginLevel()
    {
        ChangeLevelState(LevelState.Start);

    }
    IEnumerator setUp()
    {
        yield return new WaitForEndOfFrame();

        EventManager.fetchTools(levels[index]);

        yield break;
    }
}
