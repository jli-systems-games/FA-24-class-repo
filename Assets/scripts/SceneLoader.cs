using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName; // 要跳转的场景名称

    private void Update()
    {
        // 按下 Enter 键加载场景
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(nextSceneName))
        {
            LoadNextScene(nextSceneName);
        }
    }

    // 公共方法，用于通过按钮加载指定场景
    public void LoadNextScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set in the Inspector!");
        }
    }
}
