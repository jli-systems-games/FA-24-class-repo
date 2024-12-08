using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // 暂停菜单面板
    public string mainMenuSceneName; // 主菜单场景名称（可在 Inspector 中设置）
    public MonoBehaviour mouseLookScript; // 控制视角的脚本，例如 FirstPersonController

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // 暂停时间
        pauseMenuPanel.SetActive(true); // 显示暂停菜单
        Cursor.visible = true; // 显示鼠标光标
        Cursor.lockState = CursorLockMode.None; // 解锁鼠标光标
        if (mouseLookScript != null)
        {
            mouseLookScript.enabled = false; // 禁用视角控制脚本
        }
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // 恢复时间
        pauseMenuPanel.SetActive(false); // 隐藏暂停菜单
        Cursor.visible = false; // 隐藏鼠标光标
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标光标
        if (mouseLookScript != null)
        {
            mouseLookScript.enabled = true; // 启用视角控制脚本
        }
        Debug.Log("Game Resumed");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 确保时间恢复正常
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新加载当前场景
        Debug.Log("Game Restarted");
    }

    public void GoToMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            Time.timeScale = 1f; // 确保时间恢复正常
            SceneManager.LoadScene(mainMenuSceneName); // 加载主菜单场景
            Debug.Log("Returning to Main Menu: " + mainMenuSceneName);
        }
        else
        {
            Debug.LogError("Main menu scene name is not set in the Inspector!");
        }
    }
}
