using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    public float chargeRate = 20f; // ÿ�����ٶ�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("�����˽�����վ��");
            // ��ѡ����ӽ�����վ���Ӿ�����ЧЧ��
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
                // ��ѡ����������ӳ����Ӿ�����ЧЧ��
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("�������뿪���վ��");
            // ��ѡ������뿪���վ���Ӿ�����ЧЧ��
        }
    }
}
