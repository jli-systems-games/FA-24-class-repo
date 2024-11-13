using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // 移动范围
    public float moveDistance = 5f;

    // 移动速度
    public float moveSpeed = 2f;

    // 初始位置
    private Vector3 startPosition;

    void Start()
    {
        // 记录物体的初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 计算物体的新位置（基于正弦波实现左右移动）
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // 更新物体的位置
        transform.position = new Vector3(startPosition.x + offset, transform.position.y, transform.position.z);
    }
}
