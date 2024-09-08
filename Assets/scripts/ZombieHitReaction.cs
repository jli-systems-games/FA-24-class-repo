using UnityEngine;

public class ZombieHitReaction : MonoBehaviour
{
    private bool isHit = false;

    void OnTriggerEnter(Collider other) // ע������ʹ�õ���3D��Collider
    {
        // ����Ƿ��ӵ�����
        if (other.CompareTag("Bullet") && !isHit)
        {
            isHit = true; // ȷ��ִֻ��һ��
            RotateZombie();
        }
    }

    void RotateZombie()
    {
        // ��ת��ʬ��X�ᣬ��0��Ϊ90��
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
