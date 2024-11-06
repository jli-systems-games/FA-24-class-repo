using UnityEngine;
using UnityEngine.UI;

public class WeedManager : MonoBehaviour
{
    public int totalWeeds = 10; // 场景中的杂草总数
    private int currentWeedsCleared = 0;

    public Text weedCounterText; // UI显示杂草计数
    public GameObject winUI; // 胜利UI

    void Start()
    {
        UpdateWeedCounter();
        if (winUI != null)
            winUI.SetActive(false); // 初始隐藏胜利UI
    }

    public void UpdateWeedCount()
    {
        currentWeedsCleared++;
        UpdateWeedCounter();

        // 检查是否清理完成
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
        // 显示胜利UI
        if (winUI != null)
            winUI.SetActive(true);

        Debug.Log("所有杂草清理完成！");
        // 这里可以添加其他胜利逻辑，例如加载下一关
    }
}
