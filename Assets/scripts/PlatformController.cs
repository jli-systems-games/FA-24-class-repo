using UnityEngine;
using TMPro;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    public GameObject platform; // 准备平台
    public TMP_Text countdownText; // 倒计时的 TMP 文本
    public GameObject playerBall; // 小球对象
    public GameObject[] buttons; // 调整属性的按钮集合

    public void StartCountdown()
    {
        // 禁用所有按钮
        HideButtons();

        // 开始倒计时协程
        StartCoroutine(CountdownRoutine());
    }

    void HideButtons()
    {
        // 遍历所有按钮并禁用
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }

    IEnumerator CountdownRoutine()
    {
        // 倒计时 3 秒
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // 显示倒计时
            yield return new WaitForSeconds(1f);
        }

        // 倒计时结束
        countdownText.text = "Go!";
        platform.SetActive(false); // 隐藏准备平台
        yield return new WaitForSeconds(1f);

        // 清空倒计时文本
        countdownText.text = "";
    }
}
