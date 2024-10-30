using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    private bool equipped;
    public static bool slotFull;

    private Vector3 originalScale; // 保存原始缩放值

    private void Start()
    {
        // 初始化静态变量和状态
        equipped = false;
        slotFull = false;

        // 保存枪的初始缩放值
        originalScale = transform.localScale;

        // 初始化枪的状态
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

            // 设置当前持有的枪
            GunManager.SetSelectedGun(gameObject);
        }
    }

    private void Update()
    {
        // 检查玩家是否在范围内并按下E键
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

        // 设置枪为 gunContainer 的子对象，并重置局部位置、旋转，并应用初始缩放
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = originalScale; // 应用初始缩放

        // 设置物理属性
        rb.isKinematic = true;
        coll.isTrigger = true;

        // 启用枪的脚本功能
        gunScript.enabled = true;

        // 更新当前选中的枪
        GunManager.SetSelectedGun(gameObject);
        Debug.Log("Picked up: " + gameObject.name); // 打印日志确认枪已被选中
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // 将枪从手上移除并解除父对象
        transform.SetParent(null);
        transform.localScale = originalScale; // 恢复初始缩放

        // 恢复物理属性
        rb.isKinematic = false;
        coll.isTrigger = false;

        // 设置枪继承玩家移动速度
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        // 添加力量和随机旋转，使枪向前抛出
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // 禁用枪的脚本功能
        gunScript.enabled = false;

        // 清除已选择的枪
        GunManager.ClearSelectedGun();
        Debug.Log("Dropped: " + gameObject.name); // 打印日志确认枪已被放下
    }
}
