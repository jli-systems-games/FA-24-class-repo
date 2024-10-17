using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider batterySlider;
    public Slider cleanlinessSlider;
    public Slider damageSlider;
    public Button recallToChargingButton;    // 召回到充电站的按钮
    public Button recallToRepairButton;      // 召回到维修站的按钮

    private RobotStatus robotStatus;
    private RobotRecall robotRecall;

    void Start()
    {
        // 获取RobotStatus脚本的引用
        robotStatus = FindObjectOfType<RobotStatus>();
        if (robotStatus == null)
        {
            Debug.LogError("未找到 RobotStatus 脚本！");
        }

        // 获取RobotRecall脚本的引用
        robotRecall = FindObjectOfType<RobotRecall>();
        if (robotRecall == null)
        {
            Debug.LogError("未找到 RobotRecall 脚本！");
        }

        // 绑定召回到充电站按钮的点击事件
        if (recallToChargingButton != null)
        {
            recallToChargingButton.onClick.AddListener(OnRecallToChargingButtonClicked);
        }
        else
        {
            Debug.LogError("未绑定 recallToChargingButton！");
        }

        // 绑定召回到维修站按钮的点击事件
        if (recallToRepairButton != null)
        {
            recallToRepairButton.onClick.AddListener(OnRecallToRepairButtonClicked);
        }
        else
        {
            Debug.LogError("未绑定 recallToRepairButton！");
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

    void OnRecallToChargingButtonClicked()
    {
        if (robotRecall != null)
        {
            robotRecall.Recall(robotRecall.chargingStationPoint);
            Debug.Log("召回到充电站已触发。");
        }
    }

    void OnRecallToRepairButtonClicked()
    {
        if (robotRecall != null)
        {
            robotRecall.Recall(robotRecall.repairStationPoint);
            Debug.Log("召回到维修站已触发。");
        }
    }
}
