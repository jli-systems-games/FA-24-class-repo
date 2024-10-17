using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragVeggies : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (IsOverPet())
        {
            HideVeggie();
        }
        else
        {
            rectTransform.position = originalPosition;
        }
    }

    private bool IsOverPet()
    {
        RectTransform petRect = GameObject.Find("ramen pet").GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(petRect, Input.mousePosition);
    }

    private void HideVeggie()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;

        GameObject.FindObjectOfType<Stat3Game>().AddVeggie();
    }
}
