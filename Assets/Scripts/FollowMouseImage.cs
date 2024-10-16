using UnityEngine;
using UnityEngine.UI;

public class FollowMouseImage : MonoBehaviour
{
    // 引用两个 Sprite，分别是 sprite1 和 sprite2
    public Sprite sprite1;
    public Sprite sprite2;

    // 引用 UI 中的 Image 组件
    private Image image;

    void Start()
    {
        Cursor.visible = false;
        // 获取 Image 组件
        image = GetComponent<Image>();

        // 默认显示 sprite1
        image.sprite = sprite1;
    }

    void Update()
    {
        // 将 Image 的位置设置为鼠标的屏幕位置
        Vector2 mousePosition = Input.mousePosition;

        // 将屏幕坐标转换为世界坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            mousePosition,
            Camera.main,
            out Vector2 localPoint);

        // 设置 Image 的位置
        transform.localPosition = localPoint;

        // 检测鼠标左键的按下和抬起，切换 Sprite
        if (Input.GetMouseButtonDown(0))
        {
            // 按下左键切换到 sprite2
            image.sprite = sprite2;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 松开左键切换回 sprite1
            image.sprite = sprite1;
        }
    }
}
