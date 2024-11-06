using UnityEngine;

public class WeedInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false; // 玩家是否靠近
    public GameObject interactUI; // UI提示（例如“按E拔除”）
    private WeedManager weedManager; // 引用杂草管理器

    void Start()
    {
        if (interactUI != null)
            interactUI.SetActive(false); // 初始隐藏提示

        // 获取场景中的 WeedManager
        weedManager = FindObjectOfType<WeedManager>();
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
                interactUI.SetActive(true); // 显示提示
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检测玩家离开范围
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactUI != null)
                interactUI.SetActive(false); // 隐藏提示
        }
    }

    private void RemoveWeed()
    {
        // 清理杂草（销毁对象）
        Destroy(gameObject);

        // 通知 WeedManager 更新计数
        if (weedManager != null)
        {
            weedManager.UpdateWeedCount();
        }
    }
}
