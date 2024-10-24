using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;  // 引入Cinemachine命名空间

public class TextMeshProButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas homeCanvas, myCanvas;
    public CinemachineVirtualCamera homeCamera, myCamera;  // 将GameObject改为Cinemachine虚拟相机
    private TextMeshProUGUI textMeshPro;
    private AudioManager audioManager;
    private Color normalColor = Color.white;
    private Color hoverColor = Color.red;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        textMeshPro = GetComponent<TextMeshProUGUI>();

        textMeshPro.color = normalColor;

        if (myCanvas != null)
            myCanvas.gameObject.SetActive(false);  // 初始时次级菜单画布隐藏
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeTextColor(hoverColor);
        audioManager.PlayMedicPickup();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeTextColor(normalColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeTextColor(hoverColor);
        audioManager.PlayWaterPickup();

        homeCanvas.enabled = false;  // 隐藏主页画布
        if (myCanvas != null)
            myCanvas.gameObject.SetActive(true);  // 显示次级菜单画布

        // 使用Cinemachine的Priority进行相机切换
        homeCamera.Priority = 10;  // 降低主页相机的优先级
        if (myCamera != null)
            myCamera.Priority = 20;  // 提高次级相机的优先级
    }

    private void ChangeTextColor(Color color)
    {
        textMeshPro.color = color;
    }
}
