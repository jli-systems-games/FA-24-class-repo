using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public Transform player;  // ��ҵ�λ�ã��ǵ���Unity Inspector��ָ�����

    void OnMouseDown()
    {
        // �������������ľ���
        float distance = Vector3.Distance(player.position, transform.position);

        // �������С�ڵ���2���������ƻ�
        if (distance <= 2f)
        {
            // ���������İ汾
            Instantiate(destroyedVersion, transform.position, transform.rotation);

            // ���ٵ�ǰ����Ϸ����
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("�����Զ���޷��ƻ�����");
        }
    }
}
