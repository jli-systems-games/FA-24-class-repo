using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ����Ƿ�����Ҵ��������Collider
        if (other.CompareTag("Player"))
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        // ��ȡ��ǰ��������
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���¼��ص�ǰ������������Ϸ״̬
        SceneManager.LoadScene(currentSceneName);

        Debug.Log("Game Reset!");
    }
}
