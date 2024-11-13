using UnityEngine;
using System.Linq;


public class FinishLine : MonoBehaviour
{
    private int rank = 1; // 当前排名

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

        Debug.Log("All balls finished! Race complete!");
        // 在这里触发比赛结束逻辑
    }


}
