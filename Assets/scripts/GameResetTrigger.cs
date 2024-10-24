using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是玩家触发了这个Collider
        if (other.CompareTag("Player"))
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        // 获取当前场景名称
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 重新加载当前场景，重置游戏状态
        SceneManager.LoadScene(currentSceneName);

        Debug.Log("Game Reset!");
    }
}
