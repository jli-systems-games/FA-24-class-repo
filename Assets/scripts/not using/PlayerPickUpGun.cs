using UnityEngine;

public class PlayerPickUpGun : MonoBehaviour
{
    public GameObject gun; // ǹ����Ϸ����
    public Transform gunHoldPosition; // ����ʱǹ��λ�ã���������У�
    private bool isNearGun = false; // ����Ƿ񿿽�ǹ
    private bool isHoldingGun = false; // ����Ƿ����ǹ
    private Rigidbody gunRb; // ǹ��Rigidbody���

    void Start()
    {
        // ��ȡǹ��Rigidbody���
        if (gun != null)
        {
            gunRb = gun.GetComponent<Rigidbody>();
            gunRb.isKinematic = false; // ȷ��ǹһ��ʼ�������ϣ�����������Ӱ��
        }
    }
    
    void Update()
    {
        // �������ǹ�Ұ���F�������������ǹ
        if (isNearGun && Input.GetKeyDown(KeyCode.F))
        {
            if (!isHoldingGun)
            {
                PickUpGun();
            }
            else
            {
                DropGun();
            }
        }
    }

    void PickUpGun()
    {
        Debug.Log("Picked up the gun."); // ������Ϣ
        isHoldingGun = true;
        gun.transform.SetParent(gunHoldPosition); // ��ǹ��Ϊ��ҵ�������
        gun.transform.localPosition = Vector3.zero; // ��ǹ�ŵ����е�ָ��λ��
        gun.transform.localRotation = Quaternion.identity; // ����ǹ����ת
        gunRb.isKinematic = true; // ��������Ч����ʹǹ������������Ӱ��
    }

    void DropGun()
    {
        Debug.Log("Dropped the gun."); // ������Ϣ
        isHoldingGun = false;
        gun.transform.SetParent(null); // ��ǹ��������������Ƴ�
        gunRb.isKinematic = false; // ��������Ч����ʹǹ�ָ���������
        gunRb.AddForce(transform.forward * 2f, ForceMode.Impulse); // Ϊǹ���һ����΢������ʹ����ǰ�ƶ�
    }

    void OnTriggerEnter(Collider other)
    {
        // ����ҽ���ǹ�Ĵ�����Χʱ
        if (other.gameObject == gun)
        {
            isNearGun = true;
            Debug.Log("Player is near the gun."); // ������Ϣ
        }
    }

    // ֻ����һ��OnTriggerExit�������ϲ������߼�
    void OnTriggerExit(Collider other)
    {
        // ������뿪ǹ�Ĵ�����Χʱ
        if (other.gameObject == gun)
        {
            isNearGun = false;
            Debug.Log("Player left the gun's range."); // ������Ϣ
        }
    }
}
