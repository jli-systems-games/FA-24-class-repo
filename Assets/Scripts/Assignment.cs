using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment : MonoBehaviour
{
    // 三个不同的Sprite
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    // 私有的SpriteRenderer组件
    private SpriteRenderer spriteRenderer;

    // 用于标记鼠标是否悬停在物体上
    private bool isMouseOver = false;

    void Start()
    {
        // 获取当前物体的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 默认显示sprite1
        spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
        // 检查鼠标是否悬停在物体上
        if (isMouseOver)
        {
            // 如果按下左键，切换为sprite3
            if (Input.GetMouseButton(0))
            {
                spriteRenderer.sprite = sprite3;
            }
            // 如果未点击，显示sprite2
            else
            {
                spriteRenderer.sprite = sprite2;
            }
        }
        else
        {
            // 如果鼠标不在物体上，恢复到sprite1
            spriteRenderer.sprite = sprite1;
        }
    }

    // 当鼠标悬停在物体上的时候调用
    void OnMouseEnter()
    {
        isMouseOver = true;
    }

    // 当鼠标离开物体的时候调用
    void OnMouseExit()
    {
        isMouseOver = false;
    }
}
