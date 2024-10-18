using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stat2Game : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    public int shakesRequired = 10;
    private int currentShakes = 0;
    private bool hasCountedShake = false;

    public float shakeAreaY = 200f;
    private float lastPositionX;
    public float shakeThreshold = 100f;

    private AudioSource audioSource;
    public AudioClip confirmSound;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;

        audioSource = GetComponent<AudioSource>();
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
        Vector3 newPosition = new Vector3(Input.mousePosition.x, originalPosition.y, rectTransform.position.z);
        rectTransform.position = newPosition;

        float deltaX = rectTransform.position.x - originalPosition.x;

        if (Mathf.Abs(deltaX) > shakeThreshold && !hasCountedShake)
        {
            currentShakes++;
            hasCountedShake = true;
            PlayConfirmSound();
            Debug.Log(currentShakes);
        }
        else if (Mathf.Abs(deltaX) <= shakeThreshold)
        {
            hasCountedShake = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (currentShakes >= shakesRequired)
        {
            CompleteMiniGame();
        }

        currentShakes = 0;
    }

    private void PlayConfirmSound()
    {
        if (audioSource != null && confirmSound != null)
        {
            audioSource.PlayOneShot(confirmSound);
        }
    }

    private void CompleteMiniGame()
    {
        if (PetManager.Instance == null)
        {
            Debug.LogError("PetManager.Instance null");
            return;
        }
        PetManager.Instance.CompleteMiniGame2();
    }
}
