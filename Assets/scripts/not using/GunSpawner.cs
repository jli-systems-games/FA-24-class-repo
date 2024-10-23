using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public Transform gunHoldPosition; // ����ֳ�ǹ��λ��

    void Start()
    {
        // ����Ƿ���ѡ�е�ǹ
        if (GunManager.selectedGun != null)
        {
            // �������ѡ�е�ǹ
            GameObject gun = Instantiate(GunManager.selectedGun, gunHoldPosition.position, gunHoldPosition.rotation);
            gun.transform.SetParent(gunHoldPosition); // ��ǹ���ӵ��������

            // ȷ��ǹ�����λ�ú���ת������ȷ
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("No gun selected from previous scene.");
        }
    }
}
