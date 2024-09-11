using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGameManager : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("Transition Scene"); // Load the transition scene
    }
}
