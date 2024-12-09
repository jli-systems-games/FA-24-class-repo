using UnityEngine;

public class StarterSceneManager : MonoBehaviour
{
    void Start()
    {
        // 确保鼠标光标可见
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // 解锁鼠标
    }
}
