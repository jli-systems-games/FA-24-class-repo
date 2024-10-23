using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneTransition : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �������Ƿ����ǹ
            if (GunManager.selectedGun != null)
            {
                Debug.Log("Player is carrying: " + GunManager.selectedGun.name); // ������е�ǹ��
                SceneManager.LoadScene(nextSceneName); // �л�����һ������
            }
            else
            {
                Debug.Log("Player must hold a gun to enter the next scene.");
            }
        }
    }
}
