using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneSwitcher : MonoBehaviour
{
    
    public string sceneToSwitchFrom;  
    public string sceneToSwitchTo;    

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (SceneManager.GetActiveScene().name == sceneToSwitchFrom)
            {
                LoadNextScene();
            }
        }
    }

    
    void LoadNextScene()
    {
        
        SceneManager.LoadScene(sceneToSwitchTo);
    }
}
