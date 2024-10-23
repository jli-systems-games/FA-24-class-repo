using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GameObject selectedGun; // ������ҵ�ǰ���е�ǹ

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // ����GunManager�ڳ����л�ʱ������
    }

    // ������ҵ�ǰ���е�ǹ
    public static void SetSelectedGun(GameObject gun)
    {
        selectedGun = gun;
    }

    // ���ǹ����
    public static void ClearSelectedGun()
    {
        selectedGun = null;
    }
}
