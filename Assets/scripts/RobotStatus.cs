using UnityEngine;

public class RobotStatus : MonoBehaviour
{
    public float batteryLevel = 200f; // ��ʼ����
    public float cleanliness = 100f; // ��ʼ�ɾ�ֵ
    public float damageLevel = 0f; // ��ʼ�𻵳̶�

    public float batteryDecreaseRate = 1f; // ���������ٶ�
    public float cleanlinessDecreaseRate = 0.5f; // �ɾ��̶���ʱ��ļ����ٶ�
    public float cleanlinessMoveDecreaseRate = 0.3f; // �ɾ��̶����ƶ��ļ����ٶ�
    public float baseDamageIncreaseRate = 5f; // �𻵳̶ȵĻ��������ٶ�

    private bool isAlive = true; // �������Ƿ���
    private RobotMovement robotMovement; // ����RobotMovement�ű�

    void Start()
    {
        robotMovement = GetComponent<RobotMovement>(); // ��ȡRobotMovement�ű�
    }

    void Update()
    {
        if (!isAlive) return; // �����������������ֹͣһ�в���

        // 1. ��������
        batteryLevel -= batteryDecreaseRate * Time.deltaTime;
        if (batteryLevel <= 0)
        {
            batteryLevel = 0;
            RobotDeath("�����ľ�");
        }

        // 2. �ɾ��̶ȼ���
        cleanliness -= cleanlinessDecreaseRate * Time.deltaTime;

        if (robotMovement != null && robotMovement.IsMoving())
        {
            cleanliness -= cleanlinessMoveDecreaseRate * Time.deltaTime;
        }

        cleanliness = Mathf.Clamp(cleanliness, 0, 100);

        // 3. �ɾ��̶�Ӱ���𻵳̶�
        if (cleanliness < 60)
        {
            float damageRate = cleanliness < 30 ? baseDamageIncreaseRate * 2 : baseDamageIncreaseRate;
            damageLevel += damageRate * Time.deltaTime;
        }

        // 4. ����𻵳̶��Ƿ�ﵽ��������
        if (damageLevel >= 100)
        {
            damageLevel = 100;
            RobotDeath("�𻵹���");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isAlive) return; // ��������������������ٴ�����ײ

        // ��ײ����������
        if (collision.gameObject.CompareTag("Wall"))
        {
            damageLevel += baseDamageIncreaseRate;
            damageLevel = Mathf.Clamp(damageLevel, 0, 100);
        }
    }

    private void RobotDeath(string reason)
    {
        isAlive = false; // ��ǻ�����Ϊ������
        Debug.Log("������������ԭ��" + reason);
        // ������������ӻ�������������߼������粥���������������������
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    // **��������Ҫ����������**

    public void RechargeBattery(float amount)
    {
        if (!isAlive) return; // �������������������������
        batteryLevel = Mathf.Clamp(batteryLevel + amount, 0, 200);
        Debug.Log("�������ѳ�磬��ǰ������" + batteryLevel);
    }

    public void CleanRobot(float amount)
    {
        if (!isAlive) return; // ��������������������������
        cleanliness = Mathf.Clamp(cleanliness + amount, 0, 100);
        Debug.Log("����������࣬��ǰ�ɾ�ֵ��" + cleanliness);
    }

    public void RepairRobot(float amount)
    {
        if (!isAlive) return; // ���������������������������
        damageLevel = Mathf.Clamp(damageLevel - amount, 0, 100);
        Debug.Log("��������������ǰ�𻵳̶ȣ�" + damageLevel);
    }

    // ��ѡ����������˵ķ���
    public void ReviveRobot()
    {
        if (isAlive) return; // ����������Ѵ�����Ҫ����
        isAlive = true;
        batteryLevel = 100f;
        cleanliness = 100f;
        damageLevel = 0f;
        Debug.Log("�������Ѹ��");
        // ��������Ӹ������߼��������������������
    }

    // ��ѡ����RobotMovement�ű����õķ���
    public bool IsMoving()
    {
        return robotMovement != null && robotMovement.IsMoving();
    }
}
