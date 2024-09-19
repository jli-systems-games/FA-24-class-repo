using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BubblePop : MonoBehaviour
{
    public GameObject bubblePrefab;
    public RectTransform panel;

    public int bubbleCount = 12;
    public Vector2 minBubbleSize = new Vector2(50, 50);
    public Vector2 maxBubbleSize = new Vector2(150, 150);
    public float panelPadding = 10f;

    public AudioClip popSound;
    private AudioSource audioSource;

    public TextMeshProUGUI resetText;
    private List<GameObject> bubbles = new List<GameObject>();

    private void Start()
    {
        CreateBubbles();
        resetText.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void CreateBubbles()
    {
        for (int i = 0; i < bubbleCount; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab, panel);
            bubbles.Add(bubble);
            RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
            RectTransform panelRect = panel.GetComponent<RectTransform>();

            // Randomize the size of the bubble
            float randomWidth = Random.Range(minBubbleSize.x, maxBubbleSize.x);
            float randomHeight = Random.Range(minBubbleSize.y, maxBubbleSize.y);
            bubbleRect.sizeDelta = new Vector2(randomWidth, randomHeight);

            bubbleRect.pivot = new Vector2(0.5f, 0.5f);

            // Calculate available space in the panel, taking the bubble's size and padding into account
            float availableWidth = panelRect.rect.width - randomWidth - panelPadding * 2;
            float availableHeight = panelRect.rect.height - randomHeight - panelPadding * 2;

            // Random position inside the panel, with padding
            float randomX = Random.Range(panelPadding, availableWidth + panelPadding);
            float randomY = Random.Range(panelPadding, availableHeight + panelPadding);

            // Set the local position within the panel
            bubbleRect.localPosition = new Vector2(randomX - panelRect.rect.width / 2, randomY - panelRect.rect.height / 2);

            bubble.GetComponent<Button>().onClick.AddListener(() => BubbleClicked(bubble));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && bubbles.Count == 0)
        {
            CreateBubbles();
            resetText.gameObject.SetActive(false);
        }
    }

    private void BubbleClicked(GameObject bubble)
    {
        if (popSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(popSound);
        }

        Destroy(bubble);
        bubbles.Remove(bubble);

        if (bubbles.Count == 0)
        {
            resetText.gameObject.SetActive(true);
        }
    }
}