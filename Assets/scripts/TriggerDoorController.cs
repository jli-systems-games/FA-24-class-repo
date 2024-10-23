using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null; // 动画控制器
    [SerializeField] private bool openTrigger = false; // 是否触发开门
    [SerializeField] private bool closeTrigger = false; // 是否触发关门

    private void OnTriggerEnter(Collider other)
    {
        // 确保碰撞的对象是玩家
        if (other.CompareTag("Player"))
        {
            // 检查玩家是否手中有枪
            if (GunManager.selectedGun != null)
            {
                if (openTrigger)
                {
                    // 播放开门动画
                    myDoor.Play("door_open", 0, 0.0f);
                    gameObject.SetActive(false); // 触发器禁用
                }
                else if (closeTrigger)
                {
                    // 播放关门动画
                    myDoor.Play("door_close", 0, 0.0f);
                    gameObject.SetActive(false); // 触发器禁用
                }
            }
            else
            {
                // 玩家没有枪时输出日志信息
                Debug.Log("Player must have a gun to open the door.");
            }
        }
    }
}
