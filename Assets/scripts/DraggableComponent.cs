using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
