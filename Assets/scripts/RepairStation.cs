using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public float repairAmount = 30f; // 修复的损坏程度值
    public float cleanAmount = 50f; // 增加的干净值

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            RobotStatus robotStatus = other.GetComponent<RobotStatus>();
            if (robotStatus != null && robotStatus.IsAlive())
            {
                robotStatus.RepairRobot(repairAmount);
                robotStatus.CleanRobot(cleanAmount);
                Debug.Log("机器人已在维修清洁站修复和清洁。");
            }
        }
    }
}
