using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间

public class WeedInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false; // 玩家是否靠近
    public TMP_Text interactUI; // 使用 TextMeshPro 的文本组件

    void Start()
    {
        if (interactUI != null)
            interactUI.gameObject.SetActive(false); // 初始隐藏提示
    }

    void Update()
    {
        // 检测玩家按下E键并且靠近杂草
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            RemoveWeed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检测玩家进入范围
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactUI != null)
            {
                interactUI.text = "按E拔除"; // 设置提示文字
                interactUI.gameObject.SetActive(true); // 显示提示
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检测玩家离开范围
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactUI != null)
            {
                interactUI.gameObject.SetActive(false); // 隐藏提示
            }
        }
    }

    private void RemoveWeed()
    {
        // 清理杂草（销毁对象）
        Destroy(gameObject);

        // 隐藏提示UI
        if (interactUI != null)
            interactUI.gameObject.SetActive(false);

        // 如果需要通知计数器，可以添加相关逻辑
        Debug.Log("杂草已拔除！");
    }
}
