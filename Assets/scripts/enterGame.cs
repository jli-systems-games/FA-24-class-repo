using UnityEngine;
using UnityEngine.SceneManagement; 

public class enterGame : MonoBehaviour
{
   
    public string Game;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.N))
        {
           
            LoadGameScene();
        }
    }

  
    void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(Game))
        {
            SceneManager.LoadScene(Game);  // 加载指定场景
        }
        else
        {
            Debug.LogWarning("场景名称未设置！");
        }
    }
}
