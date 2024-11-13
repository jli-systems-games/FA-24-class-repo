using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // 控制旋转速度
    public float rotationSpeed = 100f;

    void Update()
    {
        // 在 Z 轴上匀速旋转
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
