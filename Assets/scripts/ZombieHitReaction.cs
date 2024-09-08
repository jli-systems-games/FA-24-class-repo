using UnityEngine;

public class ZombieHitReaction : MonoBehaviour
{
    private bool isHit = false;

    void OnTriggerEnter(Collider other) // 注意这里使用的是3D的Collider
    {
        // 检查是否被子弹击中
        if (other.CompareTag("Bullet") && !isHit)
        {
            isHit = true; // 确保只执行一次
            RotateZombie();
        }
    }

    void RotateZombie()
    {
        // 旋转僵尸的X轴，从0变为90度
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
