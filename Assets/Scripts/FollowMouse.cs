using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // 相机变量
    public Camera cam;

    void Start()
    {
        // 如果没有指定相机，使用主相机
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void Update()
    {
        // 获取当前物体的z轴值
        float zPos = transform.position.z;

        // 获取鼠标位置
        Vector3 mousePos = Input.mousePosition;

        // 将鼠标屏幕坐标转换为世界坐标
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(cam.transform.position.z - zPos)));

        // 更新物体的x和y坐标，z轴保持不变
        transform.position = new Vector3(mousePos.x, mousePos.y, zPos);
    }
}
