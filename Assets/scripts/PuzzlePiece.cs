using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 originalPosition;
    public Transform originalParent;

    void Start()
    {
        // 记录拼图块的初始位置
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 在拖动开始时，记录初始位置
        originalPosition = transform.position;
        // 将拼图块放到最前面，以便拖动时不被其他块遮挡
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 将拼图块跟随鼠标移动
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 当鼠标释放时，将块归位到初始位置或放置到最近的空格
        if (Vector3.Distance(transform.position, originalPosition) < 50f) // 假设50为距离阈值
        {
            transform.position = originalPosition;
        }
        else
        {
            // 检查是否放置在正确位置
            CheckPlacement();
        }
    }

    private void CheckPlacement()
    {
        // 添加逻辑检查是否放置在正确位置
        // 如果放置错误，可以将块移回原位
    }
}
