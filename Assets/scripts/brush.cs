using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        // Check for drawing mode with left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }

        // Check for erasing mode with right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            isErasing = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isErasing = false;
        }

        Vector2 localMousePosition;

        // Drawing functionality
        if (isDrawing && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, uiCamera, out localMousePosition))
        {
            GameObject brush = Instantiate(brushPrefab, canvasRectTransform);
            brush.GetComponent<RectTransform>().anchoredPosition = localMousePosition;

            // Start fade out coroutine for brush
            StartCoroutine(FadeAndShrink(brush));
        }

        // Erasing functionality
        if (isErasing && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, uiCamera, out localMousePosition))
        {
            GameObject eraser = Instantiate(eraserPrefab, canvasRectTransform);
            eraser.GetComponent<RectTransform>().anchoredPosition = localMousePosition;

            // Set eraser to act as a Mask for maskedImage
            if (eraser.GetComponent<Mask>() == null)
            {
                Mask mask = eraser.AddComponent<Mask>();
                mask.showMaskGraphic = false;
            }

            // Start fade out coroutine for eraser
            StartCoroutine(FadeAndShrink(eraser));
        }
    }

    // Coroutine to handle fading and shrinking effect
    private IEnumerator FadeAndShrink(GameObject obj)
    {
        // Get the RawImage component
        RawImage objRawImage = obj.GetComponent<RawImage>();

        if (objRawImage == null)
        {
            Debug.LogWarning("No RawImage component found on object: " + obj.name);
            yield break;  // Exit the coroutine if no RawImage component is found
        }

        float fadeDuration = 4f;  // Duration for the fade-out effect
        float elapsedTime = 0f;
        Color startColor = objRawImage.color;
        Vector3 startScale = obj.transform.localScale;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Update transparency (fade out)
            Color newColor = startColor;
            newColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            objRawImage.color = newColor;

            // Update scale (shrink)
            obj.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elapsedTime / fadeDuration);

            yield return null;  // Wait for the next frame
        }

        // Destroy the object after the effect is finished
        Destroy(obj);
    }
}



