using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider batterySlider;
    public Slider cleanlinessSlider;
    public Slider damageSlider;
    public Button recallToChargingButton;    // �ٻص����վ�İ�ť
    public Button recallToRepairButton;      // �ٻص�ά��վ�İ�ť

    private RobotStatus robotStatus;
    private RobotRecall robotRecall;

    void Start()
    {
        // ��ȡRobotStatus�ű�������
        robotStatus = FindObjectOfType<RobotStatus>();
        if (robotStatus == null)
        {
            Debug.LogError("δ�ҵ� RobotStatus �ű���");
        }

        // ��ȡRobotRecall�ű�������
        robotRecall = FindObjectOfType<RobotRecall>();
        if (robotRecall == null)
        {
            Debug.LogError("δ�ҵ� RobotRecall �ű���");
        }

        // ���ٻص����վ��ť�ĵ���¼�
        if (recallToChargingButton != null)
        {
            recallToChargingButton.onClick.AddListener(OnRecallToChargingButtonClicked);
        }
        else
        {
            Debug.LogError("δ�� recallToChargingButton��");
        }

        // ���ٻص�ά��վ��ť�ĵ���¼�
        if (recallToRepairButton != null)
        {
            recallToRepairButton.onClick.AddListener(OnRecallToRepairButtonClicked);
        }
        else
        {
            Debug.LogError("δ�� recallToRepairButton��");
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

    void OnRecallToChargingButtonClicked()
    {
        if (robotRecall != null)
        {
            robotRecall.Recall(robotRecall.chargingStationPoint);
            Debug.Log("�ٻص����վ�Ѵ�����");
        }
    }

    void OnRecallToRepairButtonClicked()
    {
        if (robotRecall != null)
        {
            robotRecall.Recall(robotRecall.repairStationPoint);
            Debug.Log("�ٻص�ά��վ�Ѵ�����");
        }
    }
}
