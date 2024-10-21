using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public float repairAmount = 30f; 
    public float cleanAmount = 50f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            RobotStatus robotStatus = other.GetComponent<RobotStatus>();
            if (robotStatus != null && robotStatus.IsAlive())
            {
                robotStatus.RepairRobot(repairAmount);
                robotStatus.CleanRobot(cleanAmount);
                Debug.Log("����������ά�����վ�޸�����ࡣ");
            }
        }
    }
}
