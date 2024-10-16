using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    public float chargeRate = 20f; // 每秒充电速度

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("机器人进入充电站。");
            // 可选：添加进入充电站的视觉或音效效果
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            RobotStatus robotStatus = other.GetComponent<RobotStatus>();
            if (robotStatus != null && robotStatus.IsAlive())
            {
                robotStatus.RechargeBattery(chargeRate * Time.deltaTime);
                // 可选：在这里添加充电的视觉或音效效果
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("机器人离开充电站。");
            // 可选：添加离开充电站的视觉或音效效果
        }
    }
}
