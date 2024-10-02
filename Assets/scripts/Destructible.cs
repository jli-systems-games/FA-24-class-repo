using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public Transform player;  // 玩家的位置，记得在Unity Inspector中指定玩家

    void OnMouseDown()
    {
        // 计算玩家与物体的距离
        float distance = Vector3.Distance(player.position, transform.position);

        // 如果距离小于等于2，才允许破坏
        if (distance <= 2f)
        {
            // 生成破碎后的版本
            Instantiate(destroyedVersion, transform.position, transform.rotation);

            // 销毁当前的游戏对象
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("距离过远，无法破坏物体");
        }
    }
}
