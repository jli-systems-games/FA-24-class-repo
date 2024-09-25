using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneDay = "WorldScene";  // The name of the first scene
    public string sceneNight = "NightScene";  // The name of the second scene

    private bool isDay = true;   // Tracks which scene is active

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchScene();
        }
    }

    void SwitchScene()
    {
        // Check which scene is currently active and switch to the other one
        if (isDay)
        {
            SceneManager.LoadScene(sceneNight);  // Switch to Scene 2
        }
        else
        {
            SceneManager.LoadScene(sceneDay);  // Switch back to Scene 1
        }

        // Toggle the active scene state
        isDay = !isDay;
    }
}
