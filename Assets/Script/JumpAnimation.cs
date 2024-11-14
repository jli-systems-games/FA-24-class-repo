using UnityEngine;

public class JumpAnimation : MonoBehaviour
{
    public bool isMoving = false;

    private float jumpHeight = 0.5f;    // 正弦波振幅
    private float jumpSpeed = 10f;     // 跳跃速度

    private float startY;            
    private float timeElapsed = 0f;  

    void Start()
    {
        startY = transform.localPosition.y;
    }

    void Update()
    {
        if (isMoving)
        {
            timeElapsed += Time.deltaTime * jumpSpeed;

            float yOffset = Mathf.Sin(Mathf.Abs(timeElapsed) % Mathf.PI);

            // 使用跳跃高度放大正弦波，并将物体位置设置为初始位置 + 正弦值
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                startY + yOffset * jumpHeight,
                transform.localPosition.z
            );
        }
        else
            return;
    }
}
