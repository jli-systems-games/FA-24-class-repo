using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.SceneManagement;  

public class ReloadSceneButton : MonoBehaviour
{
    public Button reloadButton; 

    void Start()
    {
        if (reloadButton != null)
        {
            reloadButton.onClick.AddListener(ReloadScene);
        }
    }

    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);

        Time.timeScale = 1; 
    }
}
