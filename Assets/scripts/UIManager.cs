using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider batterySlider;
    public Slider cleanlinessSlider;
    public Slider damageSlider;

    private RobotStatus robotStatus;

    void Start()
    {
        // 获取RobotStatus脚本的引用
        robotStatus = FindObjectOfType<RobotStatus>();

        if (robotStatus == null)
        {
            Debug.LogError("未找到 RobotStatus 脚本！");
        }
    }

    void Update()
    {
        if (robotStatus != null)
        {
            // 更新进度条的数值
            batterySlider.value = robotStatus.batteryLevel;
            cleanlinessSlider.value = robotStatus.cleanliness;
            damageSlider.value = robotStatus.damageLevel;
        }
    }
}
