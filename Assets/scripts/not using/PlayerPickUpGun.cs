using UnityEngine;

public class PlayerPickUpGun : MonoBehaviour
{
    public GameObject gun; // 枪的游戏对象
    public Transform gunHoldPosition; // 捡起时枪的位置（在玩家手中）
    private bool isNearGun = false; // 玩家是否靠近枪
    private bool isHoldingGun = false; // 玩家是否持有枪
    private Rigidbody gunRb; // 枪的Rigidbody组件

    void Start()
    {
        // 获取枪的Rigidbody组件
        if (gun != null)
        {
            gunRb = gun.GetComponent<Rigidbody>();
            gunRb.isKinematic = false; // 确保枪一开始在桌子上，可以受物理影响
        }
    }
    
    void Update()
    {
        // 如果靠近枪且按下F键，则捡起或放下枪
        if (isNearGun && Input.GetKeyDown(KeyCode.F))
        {
            if (!isHoldingGun)
            {
                PickUpGun();
            }
            else
            {
                DropGun();
            }
        }
    }

    void PickUpGun()
    {
        Debug.Log("Picked up the gun."); // 调试信息
        isHoldingGun = true;
        gun.transform.SetParent(gunHoldPosition); // 把枪设为玩家的子物体
        gun.transform.localPosition = Vector3.zero; // 把枪放到手中的指定位置
        gun.transform.localRotation = Quaternion.identity; // 重置枪的旋转
        gunRb.isKinematic = true; // 禁用物理效果，使枪不受物理引力影响
    }

    void DropGun()
    {
        Debug.Log("Dropped the gun."); // 调试信息
        isHoldingGun = false;
        gun.transform.SetParent(null); // 把枪从玩家子物体中移除
        gunRb.isKinematic = false; // 启用物理效果，使枪恢复物理引力
        gunRb.AddForce(transform.forward * 2f, ForceMode.Impulse); // 为枪添加一个轻微的力，使其向前移动
    }

    void OnTriggerEnter(Collider other)
    {
        // 当玩家进入枪的触发范围时
        if (other.gameObject == gun)
        {
            isNearGun = true;
            Debug.Log("Player is near the gun."); // 调试信息
        }
    }

    // 只保留一个OnTriggerExit方法，合并所有逻辑
    void OnTriggerExit(Collider other)
    {
        // 当玩家离开枪的触发范围时
        if (other.gameObject == gun)
        {
            isNearGun = false;
            Debug.Log("Player left the gun's range."); // 调试信息
        }
    }
}
