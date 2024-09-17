using UnityEngine;

public class keyPressed : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator 未找到，请确保 GameObject 上附加了 Animator 组件");
        }
    }

    void Update()
    {
        KeyPressed();
    }

    void KeyPressed()
    {
        // 检测按下 N 键时触发动画
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("IsClicked_N");
            animator.SetTrigger("N_Pressed");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("IsClicked_J");
            animator.SetTrigger("J_Pressed");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("IsClicked_K");
            animator.SetTrigger("K_Pressed");
        }
    }
}
