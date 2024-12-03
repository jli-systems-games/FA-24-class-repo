using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelManager : MonoBehaviour
{
    private bool pressedW = false;
    private bool pressedA = false;
    private bool pressedS = false;
    private bool pressedD = false;

    private int spacePressCount = 0; // 跳跃计数
    private bool isJumpPhase = false; // 是否进入跳跃阶段
    private bool isIgnitePhase = false; // 是否进入点燃阶段
    private int ignitedCount = 0; // 已点燃的物体计数
    private bool isComplete = false; // 是否完成任务

    public TMP_Text tutorialText; // 提示文本
    public TMP_Text completionText; // 完成提示文本
    public TMP_Text burnCountText; // 燃烧计数文本
    public GameObject[] flammableObjects; // 所有可燃物体
    public string nextSceneName; // 下一关的场景名称

    private int totalFlammableObjects; // 总燃烧物体数量

    private void Start()
    {
        totalFlammableObjects = flammableObjects.Length; // 初始化燃烧物体数量
        UpdateBurnCount();
        UpdateTutorialText();
        completionText.gameObject.SetActive(false); // 隐藏完成提示
    }

    private void Update()
    {
        if (isComplete)
        {
            // 监听 Enter 键进入下一关
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoadNextScene();
            }
            return;
        }

        if (!isJumpPhase && !isIgnitePhase)
        {
            HandleWASDInput();
        }
        else if (isJumpPhase && !isIgnitePhase)
        {
            HandleJumpInput();
        }
        else if (isIgnitePhase)
        {
            UpdateBurnedObjectsCount();
            UpdateBurnCount();

            if (AllObjectsBurned())
            {
                CompleteTutorial();
            }
        }
    }

    private void HandleWASDInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) pressedW = true;
        if (Input.GetKeyDown(KeyCode.A)) pressedA = true;
        if (Input.GetKeyDown(KeyCode.S)) pressedS = true;
        if (Input.GetKeyDown(KeyCode.D)) pressedD = true;

        UpdateTutorialText();

        if (pressedW && pressedA && pressedS && pressedD)
        {
            EnterJumpPhase();
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressCount++;
            UpdateJumpText();

            if (spacePressCount >= 3)
            {
                EnterIgnitePhase();
            }
        }
    }

    private void UpdateTutorialText()
    {
        string text = "Press ";
        if (!pressedW) text += "W ";
        if (!pressedA) text += "A ";
        if (!pressedS) text += "S ";
        if (!pressedD) text += "D ";
        tutorialText.text = text.Trim() + " to continue.";
    }

    private void UpdateJumpText()
    {
        tutorialText.text = $"Press SPACE to jump ({spacePressCount}/3)";
    }

    private void EnterJumpPhase()
    {
        isJumpPhase = true;
        tutorialText.text = "Well done! Now press SPACE 3 times to jump.";
    }

    private void EnterIgnitePhase()
    {
        isJumpPhase = false;
        isIgnitePhase = true;
        tutorialText.text = $"Great! Now ignite {totalFlammableObjects} objects.";
    }

    private void CompleteTutorial()
    {
        isComplete = true;
        tutorialText.gameObject.SetActive(false);
        completionText.gameObject.SetActive(true);
        completionText.text = "Tutorial completed! Press Enter to continue.";
        Debug.Log("Tutorial completed!");
    }

    private void UpdateBurnedObjectsCount()
    {
        ignitedCount = 0;

        foreach (var obj in flammableObjects)
        {
            if (obj != null)
            {
                var flammable = obj.GetComponent<Ignis.FlammableObject>();
                if (flammable != null && flammable.onFire)
                {
                    ignitedCount++;
                }
            }
        }
    }

    private void UpdateBurnCount()
    {
        if (burnCountText != null)
        {
            burnCountText.text = $"{ignitedCount}/{totalFlammableObjects} Burned";
        }
    }

    private bool AllObjectsBurned()
    {
        return ignitedCount >= totalFlammableObjects;
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
