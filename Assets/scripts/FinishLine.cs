using UnityEngine;
using System.Linq;

public class FinishLine : MonoBehaviour
{
    private int rank = 1; // 当前排名
    public GameObject retryButton; // Retry 按钮
    private bool raceCompleted = false; // 标记比赛是否结束

    void OnTriggerEnter(Collider other)
    {
        // 检测标签为 "Player" 或 "AI" 的对象
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            // 检查是否已经有排名（避免重复进入触发器）
            BallState ballState = other.GetComponent<BallState>();
            if (ballState != null && !ballState.finished)
            {
                // 标记为已完成
                ballState.finished = true;
                ballState.rank = rank;

                // 输出排名
                Debug.Log($"{other.name} finished with rank {rank}");
                rank++; // 更新排名

                // 如果玩家完成比赛，显示 Retry 按钮
                if (other.CompareTag("Player"))
                {
                    ShowRetryButton();
                }

                // 检查所有球是否完成比赛
                CheckAllFinished();
            }
        }
    }

    void CheckAllFinished()
    {
        // 获取所有球对象（玩家和 AI）
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Player")
            .Concat(GameObject.FindGameObjectsWithTag("AI"))
            .ToArray();

        // 检查是否有未完成的球
        foreach (GameObject ball in allBalls)
        {
            BallState ballState = ball.GetComponent<BallState>();
            if (ballState != null && !ballState.finished)
            {
                return; // 还有未完成的球
            }
        }

        // 所有球完成比赛
        if (!raceCompleted)
        {
            raceCompleted = true; // 防止多次触发
            Debug.Log("All balls finished! Race complete!");
            // 可在此添加更多逻辑（如显示比赛结束画面）
        }
    }

    void ShowRetryButton()
    {
        if (retryButton != null)
        {
            retryButton.SetActive(true); // 显示 Retry 按钮
        }
    }

    public void RetryGame()
    {
        // 重新加载当前场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
