using UnityEngine;
using UnityEngine.SceneManagement; 
public class SceneChangeOnSpace : MonoBehaviour
{
    
    [SerializeField] private string sceneName;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }

   
    void ChangeScene()
    {
        
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); 
        }
        else
        {
            Debug.LogError("Scene name not set or empty.");
        }
    }
}
