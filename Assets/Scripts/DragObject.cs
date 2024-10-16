using System.Collections;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool autoRestoreRotation = true;  // 新的布尔变量来控制是否自动恢复旋转
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private Coroutine restoreRotationCoroutine = null; // 用来存储当前的协程
    private bool isRotatingBack = false;  // 标记是否正在旋转回到(0,0,0)

    // 启动时
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 初始冻结旋转
        rb.freezeRotation = true;
        // 确保物体的初始旋转为(0,0,0)
        if (autoRestoreRotation )
        transform.rotation = Quaternion.identity;
    }

    // 当鼠标点击物体时
    void OnMouseDown()
    {
        if (!isRotatingBack) // 如果物体没有正在旋转回原始状态，允许拾取
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();

            // 解锁物体旋转
            rb.freezeRotation = false;
            rb.isKinematic = true;

            // 如果物体正在恢复旋转，取消恢复
            if (restoreRotationCoroutine != null)
            {
                StopCoroutine(restoreRotationCoroutine);
                restoreRotationCoroutine = null;
                isRotatingBack = false; // 确保状态正确
            }
        }
    }

    // 当鼠标释放时
    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;

        // 判断是否需要执行恢复旋转的逻辑
        if (autoRestoreRotation)  // 只有当 autoRestoreRotation 为 true 时才启动恢复旋转协程
        {
            // 启动协程进行3秒倒计时和旋转恢复
            restoreRotationCoroutine = StartCoroutine(RestoreRotationAfterDelay(3f, 1f));
        }
    }

    void Update()
    {
        if (isDragging)
        {
            rb.MovePosition(GetMouseWorldPosition() + offset);
        }
    }

    // 将屏幕坐标转化为世界坐标
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // 倒计时结束后1秒内恢复旋转
    private IEnumerator RestoreRotationAfterDelay(float delayTime, float smoothTime)
    {
        // 等待 delayTime 秒
        yield return new WaitForSeconds(delayTime);
        isRotatingBack = true;

        // 当前的旋转
        Quaternion currentRotation = transform.rotation;
        // 目标旋转(0, 0, 0)
        Quaternion targetRotation = Quaternion.identity;

        float elapsedTime = 0f;

        // 逐步平滑过渡
        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, elapsedTime / smoothTime);
            yield return null; // 等待下一帧
        }

        // 完成旋转后，确保完全恢复到(0,0,0)
        transform.rotation = targetRotation;

        // 冻结旋转
        rb.freezeRotation = true;
        isRotatingBack = false;
        restoreRotationCoroutine = null; // 恢复结束
    }
}
