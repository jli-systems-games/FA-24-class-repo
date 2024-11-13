using UnityEngine;
using TMPro;

public class RankDisplay : MonoBehaviour
{
    public TMP_Text rankText; // 显示排名的 TextMeshPro 文本
    public GameObject[] balls; // 所有球

    void Update()
    {
        string rankInfo = "";
        foreach (GameObject ball in balls)
        {
            BallState ballState = ball.GetComponent<BallState>();
            if (ballState != null && ballState.finished)
            {
                rankInfo += $"{ball.name}: Rank {ballState.rank}\n";
            }
        }
        rankText.text = rankInfo;
    }
}
