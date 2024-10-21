using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    public float chargeRate = 20f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("机器人进入充电站。");
            
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
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("机器人离开充电站。");
            
        }
    }
}
