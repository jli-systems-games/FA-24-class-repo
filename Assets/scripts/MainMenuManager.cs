using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName; // 在 Inspector 中设置的场景名称

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); // 加载指定场景
        }
        else
        {
            Debug.LogError("Scene name is not set in the Inspector!");
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // 退出游戏
        Debug.Log("Game has exited."); // 调试信息（在编辑器中有效）
    }
}
