using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ����ӵ���ײ
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject); // ���ٵ���
        }
    }
}
