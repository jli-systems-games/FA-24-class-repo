using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectOne()
    {
        SceneManager.LoadScene("Game1");
    }

    public void SelectTwo()
    {
        SceneManager.LoadScene("Game2");
    }

    public void SelectThree()
    {
        SceneManager.LoadScene("Game3");
    }

    public void SelectFour()
    {
        SceneManager.LoadScene("Game4");
    }
}
