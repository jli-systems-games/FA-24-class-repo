using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiElementsToHide;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUIVisibility()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        foreach (GameObject uiElement in uiElementsToHide)
        {
            if (currentSceneName == "SelectionScene") // Replace with your specific scene name
            {
                uiElement.SetActive(false); // Hide element for this scene
            }
            else
            {
                uiElement.SetActive(true); // Show element for other scenes
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUIVisibility();
    }

}
