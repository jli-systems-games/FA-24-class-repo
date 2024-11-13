using UnityEngine;
using UnityEngine.EventSystems;

public class VehicleComponentSpawner : MonoBehaviour, IPointerClickHandler
{
    public GameObject componentPrefab; 
    private GameObject currentComponent; 
    private bool isPlacing = false;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlacing) return;

        
        currentComponent = Instantiate(componentPrefab);
        isPlacing = true;
    }

    void Update()
    {
        if (isPlacing && currentComponent != null)
        {
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentComponent.transform.position = mousePos;

            
            if (Input.GetMouseButtonDown(0)) 
            {
                isPlacing = false;
            }
        }
    }
}
