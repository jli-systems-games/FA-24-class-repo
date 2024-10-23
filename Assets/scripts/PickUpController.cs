using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup: ȷ��ǹ�ĳ�ʼ״̬
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;

            // ���浱ǰ���е�ǹ
            GunManager.SetSelectedGun(gameObject);
        }
    }

    private void Update()
    {
        // ����Ƿ���Լ���ǹ
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        // �����ҳ���ǹ���Ұ���Q�������
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // ����������Ϊ��ҵ�������
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // ������������
        rb.isKinematic = true;
        coll.isTrigger = true;

        // ����ǹ�Ĺ���
        gunScript.enabled = true;

        // ���浱ǰ���е�ǹ
        GunManager.SetSelectedGun(gameObject);
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // ��ǹ����������Ƴ�
        transform.SetParent(null);

        // �ָ���������
        rb.isKinematic = false;
        coll.isTrigger = false;

        // ǹ�̳���ҵ��ƶ��ٶ�
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        // ��������������ת��ģ�����ǹ
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // ����ǹ�Ĺ���
        gunScript.enabled = false;

        // ��������ǹ����
        GunManager.ClearSelectedGun();
    }
}
