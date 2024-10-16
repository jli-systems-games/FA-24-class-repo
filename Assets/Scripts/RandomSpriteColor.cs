using UnityEngine;

public class RandomSpriteColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 随机生成颜色
        float h = Random.Range(0f, 360f) / 360f;  // Unity的HSV色相是0到1之间
        float s = Random.Range(0f, 0.75f);        // 饱和度范围0到0.75
        float v = 1.0f;                           // 亮度始终为1 (100%)

        // 使用HSV值生成RGB颜色
        Color randomColor = Color.HSVToRGB(h, s, v);

        // 应用颜色到SpriteRenderer
        spriteRenderer.color = randomColor;
    }
}
