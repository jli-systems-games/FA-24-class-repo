using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SparksManager : MonoBehaviour
{
    public float timeLimit = 60f; // 倒计时秒数
    public TMP_Text timerText; // 倒计时 TMP 文本
    public TMP_Text winText; // 胜利 TMP 文本
    public TMP_Text burnCountText; // 燃烧计数 TMP 文本
    public GameObject[] flammableObjects; // 所有可燃物体
    public string nextSceneName; // 要跳转的场景名称
    private int totalFlammableObjects; // 总燃烧物体数量
    private int burnedObjects; // 已经燃烧的物体数量
    private bool gameWon = false; // 游戏胜利状态

    private void Start()
    {
        winText.gameObject.SetActive(false); // 隐藏胜利文本
        totalFlammableObjects = flammableObjects.Length; // 初始化总燃烧物体数量
        UpdateBurnCount(); // 初始化燃烧计数显示
        UpdateTimer();
    }

    private void Update()
    {
        if (gameWon)
        {
            // 检测玩家按下回车键进入下一个场景
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoadNextScene();
            }
            return;
        }

        // 更新倒计时
        timeLimit -= Time.deltaTime;
        UpdateTimer();

        if (timeLimit <= 0)
        {
            GameOver();
        }
        else
        {
            // 检查燃烧状态
            UpdateBurnedObjectsCount();
            UpdateBurnCount();

            if (AllObjectsBurned())
            {
                WinGame();
            }
        }
    }

    private void UpdateTimer()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeLimit).ToString();
    }

    private bool AllObjectsBurned()
    {
        return burnedObjects >= totalFlammableObjects;
    }

    private void UpdateBurnedObjectsCount()
    {
        burnedObjects = 0;

        foreach (var obj in flammableObjects)
        {
            if (obj != null)
            {
                var flammable = obj.GetComponent<Ignis.FlammableObject>();
                if (flammable != null && flammable.onFire)
                {
                    burnedObjects++;
                }
            }
        }
    }

    private void UpdateBurnCount()
    {
        if (burnCountText != null)
        {
            burnCountText.text = $"{burnedObjects}/{totalFlammableObjects} Burned";
        }
    }

    private void WinGame()
    {
        gameWon = true;
        winText.gameObject.SetActive(true);
        winText.text = "You Win! Press Enter to continue.";
    }

    private void GameOver()
    {
        timerText.text = "Time: 0";
        winText.gameObject.SetActive(true);
        winText.text = "Game Over!";
    }

    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // 加载指定的场景
        }
        else
        {
            Debug.LogError("Next scene name is not set in the Inspector!");
        }
    }
}
