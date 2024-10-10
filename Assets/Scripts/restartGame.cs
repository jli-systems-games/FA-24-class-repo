using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour
{
    public void replay()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            replay();
        }
    }
}
