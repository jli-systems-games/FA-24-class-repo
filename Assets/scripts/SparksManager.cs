using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SparksManager : MonoBehaviour
{
    public float timeLimit = 60f; // 倒计时秒数
    public TMP_Text timerText; // 倒计时 TMP 文本
    public TMP_Text winText; // 胜利 TMP 文本
    public GameObject[] flammableObjects; // 所有可燃物体
    public string nextSceneName; // 要跳转的场景名称
    private bool gameWon = false; // 游戏胜利状态

    private void Start()
    {
        winText.gameObject.SetActive(false); // 隐藏胜利文本
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
        else if (AllObjectsBurned())
        {
            WinGame();
        }
    }

    private void UpdateTimer()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeLimit).ToString();
    }

    private bool AllObjectsBurned()
    {
        // 检查所有物体是否点燃
        foreach (GameObject obj in flammableObjects)
        {
            var flammable = obj.GetComponent<Ignis.FlammableObject>();
            if (flammable != null && !flammable.onFire)
                return false;
        }
        return true;
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

    private void LoadNextScene()
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
