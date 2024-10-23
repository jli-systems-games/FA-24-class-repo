using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹的Prefab
    public Transform firePoint; // 发射子弹的位置
    public float bulletSpeed = 20f; // 子弹的速度
    public float fireRate = 0.5f; // 射击间隔时间
    private float nextFireTime = 0f; // 下一次可以射击的时间

    void Update()
    {
        // 检测按下鼠标左键并且达到射击时间间隔
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // 更新下一次射击时间
            Shoot();
        }
    }

    void Shoot()
    {
        // 创建子弹对象并发射
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 调整子弹的朝向，确保子弹的Z轴与firePoint的前方一致
        bullet.transform.rotation = Quaternion.LookRotation(firePoint.forward);

        // 获取子弹的Rigidbody并设置速度
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed; // 子弹沿着firePoint的前方方向发射

        Debug.Log("Bullet fired in direction: " + firePoint.forward); // 输出调试信息
    }
}
