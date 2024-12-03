using UnityEngine;

public class SparksListener : MonoBehaviour
{
    private FlammableObject flammableObject;

    void Start()
    {
        flammableObject = GetComponent<FlammableObjectController>();

        // 检查是否存在 FlammableObjectController
        if (flammableObject != null)
        {
            // 订阅点燃事件
            flammableObject.OnIgnite += HandleObjectIgnited;
        }
    }

    void HandleObjectIgnited()
    {
        // 当物体点燃时，通知 SparksManager
        FindObjectOfType<SparksManager>().ObjectBurned();
    }
}
