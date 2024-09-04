using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Player player = Player.Noun;
    public Color p1Color;
    public Color p2Color;

    public GameObject outLine;  // 外边框物体
    private Vector3 defaultOutlineScale = new Vector3(1.3f, 1.3f, 1);  // 外边框的默认缩放大小
    public enum Player
    { 
        Noun,
        Player1,
        Player2,    
    }

    //void Update()
    //{
    //    // 获取 Rigidbody2D 组件
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();

    //    if (rb != null)
    //    {
    //        // 打印球的移动速度
    //        Debug.Log("Ball Speed: " + rb.velocity.magnitude);
    //    }
    //}


    public void SetToPlayer1()
    {
        player = Player.Player1;  // 设置玩家为Player1
        GetComponent<SpriteRenderer>().color = p1Color;  // 改变球的颜色为p1Color

        StartCoroutine(ScaleOutline());
    }

    // 改变球的颜色为p2Color，同时设置Player为Player2
    public void SetToPlayer2()
    {
        player = Player.Player2;  // 设置玩家为Player2
        GetComponent<SpriteRenderer>().color = p2Color;  // 改变球的颜色为p2Color

        StartCoroutine(ScaleOutline());
    }
    IEnumerator ScaleOutline()
    {
        if (outLine != null)
        {
            // 目标缩放大小（1, 1, 1），瞬间缩小
            Vector3 targetScale = Vector3.one;
            Vector3 defaultScale = new Vector3(1.3f, 1.3f, 1);  // 默认的缩放大小
            float duration = 0.5f;  // 动画持续时间
            float elapsedTime = 0f;

            // 瞬间将缩放设置为目标值1
            outLine.transform.localScale = targetScale;

            // 平滑放大回默认大小
            while (elapsedTime < duration)
            {
                outLine.transform.localScale = Vector3.Lerp(targetScale, defaultScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 确保缩放完全恢复到默认值
            outLine.transform.localScale = defaultScale;
        }
    }


}
