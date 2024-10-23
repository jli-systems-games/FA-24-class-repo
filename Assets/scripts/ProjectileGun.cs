using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bulletPrefab; // �ӵ���Prefab
    public Transform firePoint; // �����ӵ���λ��
    public float bulletSpeed = 20f; // �ӵ����ٶ�
    public float fireRate = 0.5f; // ������ʱ��
    private float nextFireTime = 0f; // ��һ�ο��������ʱ��

    void Update()
    {
        // ��ⰴ�����������Ҵﵽ���ʱ����
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // ������һ�����ʱ��
            Shoot();
        }
    }

    void Shoot()
    {
        // �����ӵ����󲢷���
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �����ӵ��ĳ���ȷ���ӵ���Z����firePoint��ǰ��һ��
        bullet.transform.rotation = Quaternion.LookRotation(firePoint.forward);

        // ��ȡ�ӵ���Rigidbody�������ٶ�
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed; // �ӵ�����firePoint��ǰ��������

        Debug.Log("Bullet fired in direction: " + firePoint.forward); // ���������Ϣ
    }
}
