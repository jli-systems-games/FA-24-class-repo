using UnityEngine;

public class SparksCollision : MonoBehaviour
{
    public Color woodBurningColor = Color.red; // 碰到木头时的颜色

    private void OnCollisionEnter(Collision collision)
    {
        // 检查是否碰到了木头
        if (collision.collider.CompareTag("Wood"))
        {
            // 获取木头的材质
            Renderer woodRenderer = collision.collider.GetComponent<Renderer>();
            if (woodRenderer != null)
            {
                // 修改材质颜色
                woodRenderer.material.color = woodBurningColor;

                // 如果需要着火效果，后续可以在这里加入粒子系统等
                Debug.Log("Wood is burning!");
            }
        }
    }
}
