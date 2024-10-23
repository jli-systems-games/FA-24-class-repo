using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 检查是否与子弹碰撞
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject); // 销毁敌人
        }
    }
}
