using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    
    public string targetScene;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }

    
    void ChangeScene()
    {
        
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("Ã»·Å³¡¾°");
        }
    }
}
