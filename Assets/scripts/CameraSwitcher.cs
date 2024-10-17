using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // 摄像机数组
    private int currentCameraIndex = 0; // 当前摄像机索引

    void Start()
    {
        // 确保只有当前摄像机启用，其他摄像机禁用
        for (int i = 0; i < cameras.Length; i++)
        {
            bool isCurrent = (i == currentCameraIndex);
            cameras[i].enabled = isCurrent;
            cameras[i].GetComponent<AudioListener>().enabled = isCurrent;
        }
    }

    public void SwitchCamera()
    {
        // 禁用当前摄像机
        cameras[currentCameraIndex].enabled = false;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

        // 计算下一个摄像机的索引
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // 启用下一个摄像机
        cameras[currentCameraIndex].enabled = true;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }
}
