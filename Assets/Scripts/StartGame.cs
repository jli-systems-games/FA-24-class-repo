using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    // Method to load the Customize Scene
    public void startGame()
    {
        SceneManager.LoadScene("Customize Scene"); // Replace with the actual name of your Customize Scene
    }
}
