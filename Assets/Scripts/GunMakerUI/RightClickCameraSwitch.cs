using UnityEngine;
using Cinemachine;

public class RightClickCameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera targetCamera;
    public GameObject canvasGroup;
    void Update()
    {
        // 检测右键按下
        if (Input.GetMouseButtonDown(1))  // 1表示鼠标右键
        {
            SwitchToTargetCamera();
            canvasGroup.SetActive(false);
        }

        // 检测右键松开
        if (Input.GetMouseButtonUp(1))
        {
            RestorePreviousCamera();
            canvasGroup.SetActive(true);
        }
    }

    private void SwitchToTargetCamera()
    {
        if (targetCamera != null)
        {
            // 提升目标相机的优先级，使其成为当前相机
            targetCamera.Priority = 100;  // 设定为高优先级
        }
    }

    private void RestorePreviousCamera()
    {
        if (targetCamera != null)
        {
            // 降低目标相机的优先级，停止使用该相机
            targetCamera.Priority = 0;  // 设置为较低优先级
        }
    }
}
