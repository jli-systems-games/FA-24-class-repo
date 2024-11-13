using UnityEngine;
using TMPro;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    public GameObject platform; // 准备平台
    public GameObject ball; // 球
    public TMP_Text countdownText; // 倒计时文本（TMP 类型）

    public void StartCountdown()
    {
        // 开始倒计时协程
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        // 倒计时 3 秒
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // 使用 TMP 显示文本
            yield return new WaitForSeconds(1f);
        }

        // 倒计时结束
        countdownText.text = "Go!";
        platform.SetActive(false); // 隐藏准备平台
        yield return new WaitForSeconds(1f);
        countdownText.text = ""; // 清空文本
    }
}
