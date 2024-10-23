using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneTransition : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 检查玩家是否持有枪
            if (GunManager.selectedGun != null)
            {
                SceneManager.LoadScene(nextSceneName); // 切换到下一个场景
            }
            else
            {
                Debug.Log("Player must hold a gun to enter the next scene.");
            }
        }
    }
}
