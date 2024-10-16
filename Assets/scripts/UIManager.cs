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
        // ��ȡRobotStatus�ű�������
        robotStatus = FindObjectOfType<RobotStatus>();

        if (robotStatus == null)
        {
            Debug.LogError("δ�ҵ� RobotStatus �ű���");
        }
    }

    void Update()
    {
        if (robotStatus != null)
        {
            // ���½���������ֵ
            batterySlider.value = robotStatus.batteryLevel;
            cleanlinessSlider.value = robotStatus.cleanliness;
            damageSlider.value = robotStatus.damageLevel;
        }
    }
}
