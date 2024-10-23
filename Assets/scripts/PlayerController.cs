using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform gunHoldPoint;  // 用于枪的持有位置
    public float pickupRange = 2f;  // 拾取距离
    private GameObject currentGun;  // 当前持有的枪
    private bool isHoldingGun = false;  // 玩家是否正在持有枪

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed");  // 确认F键是否检测到
            if (isHoldingGun)
            {
                DropGun();  // 放下枪
            }
            else
            {
                TryPickupGun();  // 尝试拾取枪
            }
        }
    }


    void TryPickupGun()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Gun"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                Debug.Log("Distance to gun: " + distance);  // 输出玩家和枪之间的距离

                PickUpGun(collider.gameObject);  // 拾取枪
                break;
            }
        }
    }


    void PickUpGun(GameObject gun)
    {
        currentGun = gun;
        Rigidbody gunRb = currentGun.GetComponent<Rigidbody>();

        gunRb.isKinematic = true;  // 让枪在玩家手中时不受物理引擎控制
        gunRb.useGravity = false;  // 关闭重力，防止捡起时受到重力影响

        currentGun.transform.SetParent(gunHoldPoint);  // 设置父级为玩家持枪位置
        currentGun.transform.localPosition = Vector3.zero;  // 重置位置
        currentGun.transform.localRotation = Quaternion.identity;  // 重置旋转
        isHoldingGun = true;
    }

    void DropGun()
    {
        if (currentGun != null)
        {
            Rigidbody gunRb = currentGun.GetComponent<Rigidbody>();

            gunRb.isKinematic = false;  // 恢复物理效果，允许枪受物理引擎控制
            gunRb.useGravity = true;  // 启用重力，放下后枪会掉到地面

            currentGun.transform.SetParent(null);  // 解除父级绑定
            currentGun = null;
            isHoldingGun = false;
        }
    }

}
