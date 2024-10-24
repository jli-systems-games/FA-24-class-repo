using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse1 : MonoBehaviour
{
    public RectTransform uiElement; // 需要跟随鼠标的 UI 物体
    private Canvas canvas;

    void Start()
    {
        // 获取当前对象的 Canvas 组件
        canvas = GetComponentInParent<Canvas>();
        Cursor.visible = false;
    }

    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector2 mousePosition = Input.mousePosition;

        // 将屏幕位置转换为 Canvas 的局部坐标
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out anchoredPosition);

        // 更新 UI 物体的位置
        uiElement.anchoredPosition = anchoredPosition;
    }
}
