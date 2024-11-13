using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    public Renderer ballRenderer; // 球的材质渲染器
    public PhysicMaterial ballPhysicMaterial; // 球的物理材质

    private float currentSize = 0.3f; // 初始大小
    private float currentBounciness = 0.5f; // 初始弹性

    // 更新大小
    public void IncreaseSize()
    {
        currentSize = Mathf.Min(0.5f, currentSize + 0.05f); // 防止大小超过 0.5
        UpdateSize();
    }

    public void DecreaseSize()
    {
        currentSize = Mathf.Max(0.2f, currentSize - 0.05f); // 防止大小低于 0.2
        UpdateSize();
    }

    private void UpdateSize()
    {
        transform.localScale = Vector3.one * currentSize; // 根据 currentSize 调整球大小
    }

    // 更新弹性
    public void IncreaseBounciness()
    {
        currentBounciness = Mathf.Min(1.0f, currentBounciness + 0.1f); // 防止弹性超过 1
        UpdateBounciness();
    }

    public void DecreaseBounciness()
    {
        currentBounciness = Mathf.Max(0.0f, currentBounciness - 0.1f); // 防止弹性低于 0
        UpdateBounciness();
    }

    private void UpdateBounciness()
    {
        ballPhysicMaterial.bounciness = currentBounciness;
    }

    // 更改颜色
    public void ChangeColor()
    {
        ballRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
