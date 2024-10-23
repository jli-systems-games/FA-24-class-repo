using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null; // ����������
    [SerializeField] private bool openTrigger = false; // �Ƿ񴥷�����
    [SerializeField] private bool closeTrigger = false; // �Ƿ񴥷�����

    private void OnTriggerEnter(Collider other)
    {
        // ȷ����ײ�Ķ��������
        if (other.CompareTag("Player"))
        {
            // �������Ƿ�������ǹ
            if (GunManager.selectedGun != null)
            {
                if (openTrigger)
                {
                    // ���ſ��Ŷ���
                    myDoor.Play("door_open", 0, 0.0f);
                    gameObject.SetActive(false); // ����������
                }
                else if (closeTrigger)
                {
                    // ���Ź��Ŷ���
                    myDoor.Play("door_close", 0, 0.0f);
                    gameObject.SetActive(false); // ����������
                }
            }
            else
            {
                // ���û��ǹʱ�����־��Ϣ
                Debug.Log("Player must have a gun to open the door.");
            }
        }
    }
}
