using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drag : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    private GameObject star;
    private RectTransform draggingPlane; // plane it gets dragged on? 
    private Vector2 offset;
    private bool isDragging;
    public bool isDraggable;

    public bool canGiveToPet;

    private Vector2 originalPos;

    public Inventory itemType;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isDraggable = false;
        star = this.gameObject;
        gameManager = FindObjectOfType<GameManager>();
        originalPos = this.transform.position;
        canGiveToPet = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData) // Allows dragging around the node.
    {
        if (isDraggable)
        {
            transform.position = new Vector2(
            Input.mousePosition.x,
            Input.mousePosition.y) - offset;
            isDragging = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDraggable)
        {
            star.transform.SetAsLastSibling();
            offset = new Vector2();
            //To make it so the draggables don't center on the mouse;
            offset = Input.mousePosition - transform.position;
            //Put this after as it for some reason breaks the transform code; I guess because it has to do a lot of searching before
            // the target?
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (!isDragging)
        {
            isDraggable = false;

            gameManager.updateStats(itemType);
            Destroy(gameObject);
        }

        if (canGiveToPet == false)
        {
            transform.position = originalPos;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        canGiveToPet = true;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!isDragging)
        {
            isDraggable = false;

            gameManager.updateStats(itemType);
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        canGiveToPet = false;
    }

}
