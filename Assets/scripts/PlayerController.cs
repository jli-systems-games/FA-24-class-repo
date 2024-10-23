using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform gunHoldPoint;  // ����ǹ�ĳ���λ��
    public float pickupRange = 2f;  // ʰȡ����
    private GameObject currentGun;  // ��ǰ���е�ǹ
    private bool isHoldingGun = false;  // ����Ƿ����ڳ���ǹ

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed");  // ȷ��F���Ƿ��⵽
            if (isHoldingGun)
            {
                DropGun();  // ����ǹ
            }
            else
            {
                TryPickupGun();  // ����ʰȡǹ
            }
        }
    }


    void TryPickupGun()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Gun"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                Debug.Log("Distance to gun: " + distance);  // �����Һ�ǹ֮��ľ���

                PickUpGun(collider.gameObject);  // ʰȡǹ
                break;
            }
        }
    }


    void PickUpGun(GameObject gun)
    {
        currentGun = gun;
        Rigidbody gunRb = currentGun.GetComponent<Rigidbody>();

        gunRb.isKinematic = true;  // ��ǹ���������ʱ���������������
        gunRb.useGravity = false;  // �ر���������ֹ����ʱ�ܵ�����Ӱ��

        currentGun.transform.SetParent(gunHoldPoint);  // ���ø���Ϊ��ҳ�ǹλ��
        currentGun.transform.localPosition = Vector3.zero;  // ����λ��
        currentGun.transform.localRotation = Quaternion.identity;  // ������ת
        isHoldingGun = true;
    }

    void DropGun()
    {
        if (currentGun != null)
        {
            Rigidbody gunRb = currentGun.GetComponent<Rigidbody>();

            gunRb.isKinematic = false;  // �ָ�����Ч��������ǹ�������������
            gunRb.useGravity = true;  // �������������º�ǹ���������

            currentGun.transform.SetParent(null);  // ���������
            currentGun = null;
            isHoldingGun = false;
        }
    }

}
