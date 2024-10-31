using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹的Prefab
    public Transform firePoint; // 发射子弹的位置
    public float bulletSpeed = 20f; // 子弹的速度
    public float fireRate = 0.5f; // 射击间隔时间
    private float nextFireTime = 0f; // 下一次可以射击的时间

    // 新增的旋转偏移量，可以在 Inspector 中调整
    public Vector3 rotationOffset;

    // 射击音效
    public AudioClip shootSound;
    private AudioSource audioSource; // 音频播放器

    void Start()
    {
        // 创建 AudioSource 组件并设置
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = shootSound;
    }

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

        // 将 rotationOffset 应用到子弹旋转上
        bullet.transform.rotation = Quaternion.LookRotation(firePoint.forward) * Quaternion.Euler(rotationOffset);

        // 获取子弹的 Rigidbody 并设置速度
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed; // 子弹沿着 firePoint 的前方方向发射


        // 播放射击音效
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Debug.Log("Bullet fired with rotation: " + bullet.transform.rotation.eulerAngles);
    }
}
