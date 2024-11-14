using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Image imageToFollow;     
    public RectTransform leftBottomImage;  
    public RectTransform rightTopImage;   
    public RectTransform buttonArea;
    public Camera mainCamera;

    public GameObject blueUnit;
    public GameObject redUnit;
    private enum Side
    {
     Blue,
     Red,
     None,
    }
    private Side side;
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.z = 10f;  

        imageToFollow.rectTransform.position = mouseWorldPosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(leftBottomImage, mouseScreenPosition, mainCamera))
        {
            imageToFollow.color = Color.cyan;
            side = Side.Blue;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(rightTopImage, mouseScreenPosition, mainCamera))
        {
            imageToFollow.color = Color.red;
            side = Side.Red;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(buttonArea, mouseScreenPosition, mainCamera))
        {
            imageToFollow.color = Color.white;
            side = Side.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0f;  // 设置Z轴为0，以避免单位生成在错误的深度

            switch (side)
            {
                case Side.Red:
                    if(redUnit != null)
                    Instantiate(redUnit, spawnPosition, Quaternion.identity);
                    break;
                case Side.Blue:
                    if (blueUnit != null)
                        Instantiate(blueUnit, spawnPosition, Quaternion.identity);
                    break;
                case Side.None:
                    return;
            }
        }

    }
}
