using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotate : MonoBehaviour
{
    // ����Animator���
    private Animator animator;

    void Start()
    {
        // ��ȡAnimator���
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ��������������ʱ
        if (Input.GetMouseButtonDown(0))
        {
            // ���ò���������
            animator.ResetTrigger("axe_rotate");  // ������Trigger����ȷ��ÿ�ε���������´���
            animator.SetTrigger("axe_rotate");    // ��������
        }
    }
}
