using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    public GameObject[] decorations; // 装饰物 Prefab 数组
    public Transform playerCamera; // 玩家摄像机
    public float maxPlacementDistance = 5.0f; // 最大放置距离
    private GameObject currentDecoration; // 当前生成的装饰物
    public LayerMask placementMask; // 可放置的层

    public float rotationSpeed = 100.0f; // 旋转速度

    void Update()
    {
        // 检测数字键选择物品
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectDecoration(0); // 按键2选择第一个物品
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectDecoration(1); // 按键3选择第二个物品
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectDecoration(2); // 按键4选择第三个物品
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectDecoration(3); // 按键5选择第四个物品
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectDecoration(4); // 按键6选择第五个物品
        }

        // 如果有当前物品
        if (currentDecoration != null)
        {
            PreviewDecoration();

            // 使用 Q 和 E 旋转物品
            RotateDecoration();

            // 按下鼠标左键确认放置
            if (Input.GetMouseButtonDown(0))
            {
                PlaceDecoration();
            }

            // 按下鼠标右键取消
            if (Input.GetMouseButtonDown(1))
            {
                CancelDecoration();
            }
        }
    }

    public void SelectDecoration(int index)
    {
        // 如果已有当前物品，销毁它
        if (currentDecoration != null)
        {
            Destroy(currentDecoration);
        }

        // 创建新物品
        if (index >= 0 && index < decorations.Length)
        {
            currentDecoration = Instantiate(decorations[index]);
            currentDecoration.GetComponent<Collider>().enabled = false; // 禁用碰撞，避免干扰预览
        }
    }

    private void PreviewDecoration()
    {
        // 射线从摄像机向前发出
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxPlacementDistance, placementMask))
        {
            // 将装饰物移动到射线命中的位置
            currentDecoration.transform.position = hit.point;

            // 面朝地形或目标的法线方向
            currentDecoration.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void RotateDecoration()
    {
        // 按 Q 逆时针旋转，按 E 顺时针旋转
        if (Input.GetKey(KeyCode.Q))
        {
            currentDecoration.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentDecoration.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    private void PlaceDecoration()
    {
        // 确认放置
        if (currentDecoration != null)
        {
            currentDecoration.GetComponent<Collider>().enabled = true; // 启用碰撞
            currentDecoration = null;
        }
    }

    private void CancelDecoration()
    {
        // 取消当前物品
        if (currentDecoration != null)
        {
            Destroy(currentDecoration);
        }
    }
}
