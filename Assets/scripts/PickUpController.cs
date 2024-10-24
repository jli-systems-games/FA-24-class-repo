using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        // 确保在每次开始时，静态变量和状态被正确初始化
        equipped = false;
        slotFull = false;

        // 设置枪的初始状态
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        else
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;

            // 保存当前持有的枪
            GunManager.SetSelectedGun(gameObject);
        }
    }

    private void Update()
    {
        // 检查玩家是否靠近并按下E键捡起枪
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        // 检查玩家是否按下Q键放下枪
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // 将武器设置为玩家的子物体
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // 设置物理属性
        rb.isKinematic = true;
        coll.isTrigger = true;

        // 启用枪的功能
        gunScript.enabled = true;

        GunManager.SetSelectedGun(gameObject);
        Debug.Log("Picked up: " + gameObject.name); // 打印日志确认枪已被选中
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // 将枪从玩家手上移除
        transform.SetParent(null);

        // 恢复物理属性
        rb.isKinematic = false;
        coll.isTrigger = false;

        // 枪继承玩家的移动速度
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        // 添加力量和随机旋转以模拟放下枪
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // 禁用枪的功能
        gunScript.enabled = false;

        // 清除保存的枪数据
        GunManager.ClearSelectedGun();
    }
}
