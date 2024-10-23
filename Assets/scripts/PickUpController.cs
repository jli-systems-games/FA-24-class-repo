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
        //Setup: 确定枪的初始状态
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
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
        // 检查是否可以捡起枪
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        // 如果玩家持有枪并且按下Q键则放下
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

        // 保存当前持有的枪
        GunManager.SetSelectedGun(gameObject);
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
