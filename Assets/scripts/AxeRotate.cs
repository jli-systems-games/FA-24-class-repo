using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotate : MonoBehaviour
{
    // 引用Animator组件
    private Animator animator;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 当鼠标左键被按下时
        if (Input.GetMouseButtonDown(0))
        {
            // 重置并触发动画
            animator.ResetTrigger("axe_rotate");  // 先重置Trigger，以确保每次点击都能重新触发
            animator.SetTrigger("axe_rotate");    // 触发动画
        }
    }
}
