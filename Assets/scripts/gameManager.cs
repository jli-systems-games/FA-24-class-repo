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

        else
        {
            Debug.Log("not a level");
        }
        
    }
}
