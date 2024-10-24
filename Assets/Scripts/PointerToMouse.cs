using UnityEngine;

public class PointerToMouse : MonoBehaviour
{
    public bool canMove = true;
    // 目标物体
    public Transform target;

    // SpriteRenderer组件
    private SpriteRenderer spriteRenderer;
    public Transform[] gunparts;
    private float[] initialYPositions;

    public GameObject bullet;
    public GameObject shell;
    public GameObject meg;
    public GameObject grenade;

    public Transform bulletAnchor,shellAnchor,megAnchor;
    public Transform barrelTransform;
    private float bulletAnchorXpos;

    private Animator animator;
    private float spreadAngle;
    private float projectileSpeed = 18f;
    private GunsmithData gunsmithData;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gunsmithData = FindObjectOfType<GunsmithData>();
    }
    void Start()
    {
        switch (gunsmithData.barrelType)
        {
            case GunsmithData.BarrelType.ShortS:
                barrelTransform.localPosition = new Vector3(0.5f, 0.03f, transform.localPosition.z);
                bulletAnchorXpos = 0.965f;
                break;
            case GunsmithData.BarrelType.ShortM:
                barrelTransform.localPosition = new Vector3(0.6f, 0.03f, transform.localPosition.z);
                bulletAnchorXpos = 1.05f;
                break;
            case GunsmithData.BarrelType.ShortL:
                barrelTransform.localPosition = new Vector3(0.731f, 0.03f, transform.localPosition.z);
                bulletAnchorXpos = 1.168f;
                break;
            case GunsmithData.BarrelType.LongS:
                barrelTransform.localPosition = new Vector3(0.635f, 0.03f, transform.localPosition.z);
                bulletAnchorXpos = 1.072f;
                break;
            case GunsmithData.BarrelType.LongM:
                barrelTransform.localPosition = new Vector3(0.73f, 0.03f, transform.localPosition.z);
                bulletAnchorXpos = 1.175f;
                break;
            case GunsmithData.BarrelType.LongL:
                barrelTransform.localPosition = new Vector3(0.86f, 0.03f, transform.localPosition.z);
                bulletAnchorXpos = 1.3f;
                break;
        }

        initialYPositions = new float[gunparts.Length];
        
        for (int i = 0; i < gunparts.Length; i++)
        {
            initialYPositions[i] = gunparts[i].localPosition.y;
        }

    }

    void Update()
    {
        if (target != null && canMove)
        {
            // 计算目标物体与当前物体的相对位置
            Vector3 direction = target.position - transform.position;

            // 只在XY平面计算方向，将z轴方向设为0
            direction.z = 0;

            // 计算出指向目标物体的角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 通过沿z轴的旋转，使物体朝向目标物体
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // 根据当前物体的x坐标和目标物体的x坐标决定是否翻转sprite
            if (transform.position.x > target.position.x)
            {
                spriteRenderer.flipY = true;  // 翻转X轴
                foreach (Transform sprite in gunparts)
                {
                    Vector3 scale = sprite.localScale;
                    // 修改 x 轴的值为 -1
                    scale.y = -1;
                    // 将修改后的 scale 应用回去
                    sprite.localScale = scale;
                }
                for (int i = 0; i < gunparts.Length; i++)
                {
                    Vector3 localPos = gunparts[i].localPosition;
                    localPos.y = -initialYPositions[i];  // 将局部 y 轴位置变为 -初始值
                    gunparts[i].localPosition = localPos;
                }
                bulletAnchor.transform.localPosition = new Vector3(bulletAnchorXpos, -0.1f, transform.localPosition.z);
            }
            else
            {
                spriteRenderer.flipY = false;  // 不翻转X轴
                foreach (Transform sprite in gunparts)
                {
                    Vector3 scale = sprite.localScale;
                    // 修改 x 轴的值为 -1
                    scale.y = 1;
                    // 将修改后的 scale 应用回去
                    sprite.localScale = scale;
                }
                for (int i = 0; i < gunparts.Length; i++)
                {
                    Vector3 localPos = gunparts[i].localPosition;
                    localPos.y = initialYPositions[i];  // 将局部 y 轴位置变为 -初始值
                    gunparts[i].localPosition = localPos;
                }
                bulletAnchor.transform.localPosition = new Vector3(bulletAnchorXpos, 0.1f, transform.localPosition.z);
            }
        }        
    }

    public void ShootProjectile(GameObject bulletType)
    {
        if (bullet != null && shell != null)
        {
            animator.SetTrigger("Trigger");
            // 在当前物体的位置生成物体
            GameObject projectile = Instantiate(bulletType, bulletAnchor.position, transform.rotation);
            GameObject shellPrefab = Instantiate(shell, shellAnchor.position, Quaternion.identity);

            // 获取生成物体的刚体组件
            Rigidbody rbBullet = projectile.GetComponent<Rigidbody>();
            Rigidbody rbShell = shellPrefab.GetComponent<Rigidbody>();

            Vector2 shootDirection = transform.right; // 原始发射方向
            float randomAngle = Random.Range(-spreadAngle, spreadAngle); // 在 [-spreadAngle, spreadAngle] 范围内生成随机角度

            // 创建新的方向矢量，应用角度偏移
            Vector2 offsetDirection = Quaternion.Euler(0, 0, randomAngle) * shootDirection;

            // 应用新的旋转角度来调整子弹的朝向，并补偿90°的旋转
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, offsetDirection) * Quaternion.Euler(0, 0, -90);
            projectile.transform.rotation = bulletRotation; // 更新子弹朝向

            // 设置子弹的速度
            rbBullet.velocity = offsetDirection * projectileSpeed;

            // 生成一个随机的方向值，模拟出弹壳乱飞的效果
            Vector2 randomDirection = Random.insideUnitCircle.normalized; // 随机方向
            float randomVerticalOffset = Random.Range(0.5f, 1.5f); // 随机的垂直偏移
            Vector3 shellVelocity = new Vector3(randomDirection.x, randomVerticalOffset, randomDirection.y) * 1;

            rbShell.velocity = shellVelocity;
        }
    }
    public void ShootGrenade()
    {
        if (grenade != null)
        {
            animator.SetTrigger("Trigger");
            // 在当前物体的位置生成物体
            GameObject projectile = Instantiate(grenade, bulletAnchor.position, transform.rotation);

            // 获取生成物体的刚体组件
            Rigidbody rbBullet = projectile.GetComponent<Rigidbody>();

            Vector2 shootDirection = transform.right; // 原始发射方向
            float randomAngle = Random.Range(-spreadAngle, spreadAngle); // 在 [-spreadAngle, spreadAngle] 范围内生成随机角度

            // 创建新的方向矢量，应用角度偏移
            Vector2 offsetDirection = Quaternion.Euler(0, 0, randomAngle) * shootDirection;

            // 应用新的旋转角度来调整子弹的朝向，并补偿90°的旋转
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, offsetDirection) * Quaternion.Euler(0, 0, 90);
            projectile.transform.rotation = bulletRotation; 

            // 设置子弹的速度
            rbBullet.velocity = offsetDirection * 6;
        }
    }
    public void UpdateSpreadAngle(float value)
    { spreadAngle = value; }
    public void SpawnMeg()
    {
        if (meg != null)
        {
           Instantiate(meg, megAnchor.position, Quaternion.identity);           
        }
    }
}
