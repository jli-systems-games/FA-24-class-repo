using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherTMP : MonoBehaviour
{

    public string sceneName;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            LoadGameScene();
        }
    }


    void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);  // ����ָ������
        }
        else
        {
            Debug.LogWarning("��������δ���ã�");
        }
    }
}


