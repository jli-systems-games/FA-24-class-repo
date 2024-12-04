using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public beam reachedTarget; // checks 'won' bool

    public bool level2Key;
    public bool level3Key;
    public bool level4Key;
    public bool level5Key;
    public bool level6Key;
    public bool level7Key;
    public bool level8Key;
    public bool level9Key;
    public bool level10Key;
    public bool level11Key;
    public bool level12Key;
    public bool level13Key;
    public bool level14Key;
    public bool level15Key;
    public bool level16Key;
    public bool level17Key;
    public bool level18Key;
    public bool level19Key; 
    public bool level20Key;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        reachedTarget = GameObject.FindWithTag("beamCheck").GetComponent<beam>();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level1") && reachedTarget.won == true && level2Key == false)
        {
            level2Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level2") && reachedTarget.won == true && level3Key == false)
        {
            level3Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level3") && reachedTarget.won == true && level4Key == false)
        {
            level4Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level4") && reachedTarget.won == true && level5Key == false)
        {
            level5Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level5") && reachedTarget.won == true && level6Key == false)
        {
            level6Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level6") && reachedTarget.won == true && level7Key == false)
        {
            level7Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level7") && reachedTarget.won == true && level8Key == false)
        {
            level8Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level8") && reachedTarget.won == true && level9Key == false)
        {
            level9Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level9") && reachedTarget.won == true && level10Key == false)
        {
            level10Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level10") && reachedTarget.won == true && level11Key == false)
        {
            level11Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level11") && reachedTarget.won == true && level12Key == false)
        {
            level12Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level12") && reachedTarget.won == true && level13Key == false)
        {
            level13Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level13") && reachedTarget.won == true && level14Key == false)
        {
            level14Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level14") && reachedTarget.won == true && level15Key == false)
        {
            level15Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level15") && reachedTarget.won == true && level16Key == false)
        {
            level16Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level16") && reachedTarget.won == true && level17Key == false)
        {
            level17Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level17") && reachedTarget.won == true && level18Key == false)
        {
            level18Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level18") && reachedTarget.won == true && level19Key == false)
        {
            level19Key = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("level19") && reachedTarget.won == true && level20Key == false)
        {
            level20Key = true;
        }

        else
        {
            Debug.Log("not a level");
        }
        
    }
}
