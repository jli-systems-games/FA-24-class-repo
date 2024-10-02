using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public Transform player;  // ��ҵ�λ�ã��ǵ���Unity Inspector��ָ�����

    void OnMouseDown()
    {
        
        float distance = Vector3.Distance(player.position, transform.position);

        
        if (distance <= 3f)
        {
            
            Instantiate(destroyedVersion, transform.position, transform.rotation);

            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("�����Զ���޷��ƻ�����");
        }
    }
}
