using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeLimit : MonoBehaviour
{
    public Transform playerTransform; 
    private Image image;
    public PlayerController playerController;
    public float edgeDecayStart = 8f; 
    public float edgeDecayMax = 10f;  

    private float edgeDecayStartL;    
    private float edgeDecayMaxL;      
    private float edgeDecayStartR;    
    private float edgeDecayMaxR;      

    void Start()
    {
        edgeDecayStartL = -edgeDecayStart;
        edgeDecayMaxL = -edgeDecayMax;
        edgeDecayStartR = edgeDecayStart;
        edgeDecayMaxR = edgeDecayMax;

        image = GetComponent<Image>(); 
        Color tempColor = image.color;
        tempColor.a = 0f;              
        image.color = tempColor;
    }

    void Update()
    {
        float playerX = playerTransform.position.x; // 获取玩家的 x 坐标
        float alpha = 0f; // 初始化 alpha 值为 0

        if (playerX > edgeDecayStartR)
        {
            // 右边界逻辑，玩家在右侧，开始增加 alpha
            if (playerX <= edgeDecayMaxR)
            {
                playerController.canGoRight = true;
                // 玩家位于 edgeDecayStart 和 edgeDecayMax 之间，alpha 增加
                float t = (playerX - edgeDecayStartR) / (edgeDecayMaxR - edgeDecayStartR);
                alpha = Mathf.Lerp(0f, 1f, t);
            }
            else
            {
                playerController.canGoRight = false;
                // 玩家超过了 edgeDecayMax，alpha 直接为 1
                alpha = 1f;
            }
        }
        else if (playerX < edgeDecayStartL)
        {
            // 左边界逻辑，玩家在左侧，开始增加 alpha
            if (playerX >= edgeDecayMaxL)
            {
                // 玩家位于 edgeDecayStart 和 edgeDecayMax 之间，alpha 增加
                float t = (edgeDecayStartL - playerX) / (edgeDecayStartL - edgeDecayMaxL);
                alpha = Mathf.Lerp(0f, 1f, t);
                playerController.canGoLeft = true;
            }
            else
            {
                playerController.canGoLeft = false;
                // 玩家超过了 edgeDecayMax，alpha 直接为 1
                alpha = 1f;
            }
        }

        // 设置 Image 组件的 alpha 值
        Color tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }
}
