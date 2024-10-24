using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomControl : MonoBehaviour
{
    public float maxRange;  // 相机距离的最大范围
    private float scrollSpeed = 5f;  // 鼠标滚轮缩放的速度
    private float minRange = 6.4f;  // 相机距离的最小范围
    private CinemachineFramingTransposer framingTransposer;
    public PlayerData playerData;
    private GunsmithData gunsmithData;

    
    void Start()
    {
        gunsmithData = playerData.gunsmithData;
        switch (gunsmithData.scopeType)
        {
            case GunsmithData.ScopeType.IronSights:
                maxRange = 10f;
                break;
            case GunsmithData.ScopeType.Holographic:
                maxRange = 13f;
                break;
            case GunsmithData.ScopeType.MidRange:
                maxRange = 16f;
                break;
            case GunsmithData.ScopeType.LongRange:
                maxRange = 21f;
                break;
        }
        CinemachineVirtualCamera vcam = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

        // 初始化相机距离，设置为最小距离
        framingTransposer.m_CameraDistance = minRange;
    }

    void Update()
    {
        // 获取滚轮输入
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            // 调整相机距离，使用滚轮输入影响相机距离
            framingTransposer.m_CameraDistance -= scrollInput * scrollSpeed;

            // 限制相机距离在最小值和最大值之间
            framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, minRange, maxRange);
        }
    }
}
