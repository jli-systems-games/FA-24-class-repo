using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public Button resetButton;

    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetGame()
    {
        // Reload the current active scene, resetting the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
