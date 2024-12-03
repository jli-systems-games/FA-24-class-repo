using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SparksManager : MonoBehaviour
{
    public float timeLimit = 60f; // 倒计时秒数
    public GameObject[] flammableObjects; // 可点燃的物体
    public Text timerText; // 显示倒计时的文本
    public Text winText; // 胜利文字
    private int objectsBurned = 0; // 已点燃的物体计数
    private bool gameWon = false;

    void Start()
    {
        // 初始化倒计时
        UpdateTimerText();
        winText.gameObject.SetActive(false); // 隐藏胜利文字
    }

    void Update()
    {
        if (!gameWon)
        {
            // 更新倒计时
            timeLimit -= Time.deltaTime;
            UpdateTimerText();

            // 如果时间用完且游戏未完成
            if (timeLimit <= 0)
            {
                GameOver();
            }
        }

        // 按下回车键进入下一场景
        if (gameWon && Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextScene();
        }
    }

    public void ObjectBurned()
    {
        // 更新已点燃物体计数
        objectsBurned++;

        // 检查是否所有物体都点燃了
        if (objectsBurned >= flammableObjects.Length)
        {
            WinGame();
        }
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeLimit).ToString();
    }

    void WinGame()
    {
        gameWon = true;
        winText.gameObject.SetActive(true); // 显示胜利文字
        winText.text = "You Win!"; // 设置胜利信息
    }

    void GameOver()
    {
        if (!gameWon)
        {
            Debug.Log("Game Over! 时间用完。");
            // 可在这里添加失败逻辑
        }
    }

    public void LoadNextScene()
    {
        // 加载下一个场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
