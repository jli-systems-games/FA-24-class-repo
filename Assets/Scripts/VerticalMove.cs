using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMove : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 移动速度
    public float moveRange = 0.5f;  // 移动范围

    private Vector3 startingPosition; // 起始位置
    private float time;

    void Start()
    {
        // 记录物体的起始位置
        startingPosition = transform.localPosition;
    }

    void Update()
    {
        // 计算当前时间并基于正弦函数来获取上下移动的偏移
        time += Time.deltaTime * moveSpeed;
        float offset = Mathf.Sin(time) * moveRange;

        // 更新物体的本地位置
        transform.localPosition = startingPosition + new Vector3(0, offset, 0);
    }
}
