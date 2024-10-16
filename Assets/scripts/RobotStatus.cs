using UnityEngine;

public class RobotStatus : MonoBehaviour
{
    public float batteryLevel = 200f; // 初始电量
    public float cleanliness = 100f; // 初始干净值
    public float damageLevel = 0f; // 初始损坏程度

    public float batteryDecreaseRate = 1f; // 电量消耗速度
    public float cleanlinessDecreaseRate = 0.5f; // 干净程度随时间的减少速度
    public float cleanlinessMoveDecreaseRate = 0.3f; // 干净程度随移动的减少速度
    public float baseDamageIncreaseRate = 5f; // 损坏程度的基本增加速度

    private bool isAlive = true; // 机器人是否存活
    private RobotMovement robotMovement; // 引用RobotMovement脚本

    void Start()
    {
        robotMovement = GetComponent<RobotMovement>(); // 获取RobotMovement脚本
    }

    void Update()
    {
        if (!isAlive) return; // 如果机器人已死亡，停止一切操作

        // 1. 电量减少
        batteryLevel -= batteryDecreaseRate * Time.deltaTime;
        if (batteryLevel <= 0)
        {
            batteryLevel = 0;
            RobotDeath("电量耗尽");
        }

        // 2. 干净程度减少
        cleanliness -= cleanlinessDecreaseRate * Time.deltaTime;

        if (robotMovement != null && robotMovement.IsMoving())
        {
            cleanliness -= cleanlinessMoveDecreaseRate * Time.deltaTime;
        }

        cleanliness = Mathf.Clamp(cleanliness, 0, 100);

        // 3. 干净程度影响损坏程度
        if (cleanliness < 60)
        {
            float damageRate = cleanliness < 30 ? baseDamageIncreaseRate * 2 : baseDamageIncreaseRate;
            damageLevel += damageRate * Time.deltaTime;
        }

        // 4. 检查损坏程度是否达到死亡条件
        if (damageLevel >= 100)
        {
            damageLevel = 100;
            RobotDeath("损坏过大");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isAlive) return; // 如果机器人已死亡，不再处理碰撞

        // 碰撞导致损坏增加
        if (collision.gameObject.CompareTag("Wall"))
        {
            damageLevel += baseDamageIncreaseRate;
            damageLevel = Mathf.Clamp(damageLevel, 0, 100);
        }
    }

    private void RobotDeath(string reason)
    {
        isAlive = false; // 标记机器人为已死亡
        Debug.Log("机器人死亡，原因：" + reason);
        // 可以在这里添加机器人死亡后的逻辑，例如播放死亡动画、禁用组件等
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    // **以下是需要的三个方法**

    public void RechargeBattery(float amount)
    {
        if (!isAlive) return; // 如果机器人已死亡，不允许充电
        batteryLevel = Mathf.Clamp(batteryLevel + amount, 0, 200);
        Debug.Log("机器人已充电，当前电量：" + batteryLevel);
    }

    public void CleanRobot(float amount)
    {
        if (!isAlive) return; // 如果机器人已死亡，不允许清洁
        cleanliness = Mathf.Clamp(cleanliness + amount, 0, 100);
        Debug.Log("机器人已清洁，当前干净值：" + cleanliness);
    }

    public void RepairRobot(float amount)
    {
        if (!isAlive) return; // 如果机器人已死亡，不允许修理
        damageLevel = Mathf.Clamp(damageLevel - amount, 0, 100);
        Debug.Log("机器人已修理，当前损坏程度：" + damageLevel);
    }

    // 可选：复活机器人的方法
    public void ReviveRobot()
    {
        if (isAlive) return; // 如果机器人已存活，不需要复活
        isAlive = true;
        batteryLevel = 100f;
        cleanliness = 100f;
        damageLevel = 0f;
        Debug.Log("机器人已复活！");
        // 在这里添加复活后的逻辑，例如重新启用组件等
    }

    // 可选：供RobotMovement脚本调用的方法
    public bool IsMoving()
    {
        return robotMovement != null && robotMovement.IsMoving();
    }
}
