using UnityEngine;

public class DragFruit : MonoBehaviour
{
    Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition()
    {
       
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; 
        return mouseWorldPosition;
    }

    private void OnMouseDown()
    {
        
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }
}
