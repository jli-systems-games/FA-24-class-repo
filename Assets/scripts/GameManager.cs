using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 存储所有微型游戏的引用
    public GameObject[] minigames; // 将你的所有微型游戏在Inspector中拖入这个数组
    private int currentMinigameIndex = 0; // 当前正在进行的微型游戏索引

    void Start()
    {
        // 初始化，隐藏所有微型游戏
        foreach (GameObject minigame in minigames)
        {
            minigame.SetActive(false);
        }

        // 启动第一个微型游戏
        StartMinigame(currentMinigameIndex);
    }

    // 启动指定的微型游戏
    public void StartMinigame(int index)
    {
        if (index >= 0 && index < minigames.Length)
        {
            // 显示当前微型游戏
            minigames[index].SetActive(true);
        }
    }

    // 结束当前微型游戏，启动下一个
    public void EndCurrentMinigame()
    {
        // 隐藏当前微型游戏
        minigames[currentMinigameIndex].SetActive(false);

        // 切换到下一个微型游戏
        currentMinigameIndex++;
        if (currentMinigameIndex < minigames.Length)
        {
            StartMinigame(currentMinigameIndex);
        }
        else
        {
            Debug.Log("所有微型游戏已完成！");
            // 所有微型游戏完成后的处理，可以回到主界面或者显示总得分
        }
    }
}
