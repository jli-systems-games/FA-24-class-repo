using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Method to go back to the Customization Scene
    public void BackToCustomization()
    {
        SceneManager.LoadScene("Customize Scene"); // Replace with the actual name of your Customization Scene
    }
}
