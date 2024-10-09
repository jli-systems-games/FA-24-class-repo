using UnityEngine;
using UnityEngine.UI;

public class brush : MonoBehaviour
{
    public GameObject brushPrefab;  
    public GameObject eraserPrefab; 
    public RectTransform canvasRectTransform;  
    public Camera uiCamera;  

    private bool isDrawing = false;  
    private bool isErasing = false;

    public MaskableGraphic maskedImage;  

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
        }

       
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }

        
        if (Input.GetMouseButtonDown(1))
        {
            isErasing = true;
        }

       
        if (Input.GetMouseButtonUp(1))
        {
            isErasing = false;
        }

        
        if (isDrawing)
        {
            Vector2 localMousePosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, uiCamera, out localMousePosition))
            {
                
                GameObject brush = Instantiate(brushPrefab, canvasRectTransform);
                brush.GetComponent<RectTransform>().anchoredPosition = localMousePosition;
            }
        }

       
        if (isErasing)
        {
            Vector2 localMousePosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, uiCamera, out localMousePosition))
            {
                
                GameObject eraser = Instantiate(eraserPrefab, canvasRectTransform);
                eraser.GetComponent<RectTransform>().anchoredPosition = localMousePosition;

                
                maskedImage.GetComponent<Mask>().enabled = true;
            }
        }
    }
}

