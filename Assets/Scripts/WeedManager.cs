using UnityEngine;
using TMPro;

public class WeedManager : MonoBehaviour
{
    public int totalWeeds = 10; // 场景中的杂草总数
    private int currentWeedsCleared = 0;

    public TMP_Text weedCounterText; // 显示杂草计数
    public TMP_Text winText; // 胜利文字

    void Start()
    {
        UpdateWeedCounter();
        if (winText != null)
            winText.gameObject.SetActive(false); // 初始隐藏胜利文字
    }

    public void UpdateWeedCount()
    {
        currentWeedsCleared++;
        UpdateWeedCounter();

        if (currentWeedsCleared >= totalWeeds)
        {
            CompleteTask();
        }
    }

    private void UpdateWeedCounter()
    {
        if (weedCounterText != null)
            weedCounterText.text = $"清理杂草：{currentWeedsCleared}/{totalWeeds}";
    }

    private void CompleteTask()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(true); // 显示胜利文字
            winText.text = "胜利！";
        }

        Debug.Log("所有杂草清理完成！");
    }
}
