using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Breaking : MonoBehaviour
{
    [Header("Boxes")]
    public GameObject QBox;
    public GameObject WBox;
    public GameObject EBox;
    public GameObject RBox;

    [Header("Shrimp Sprites")]
    public Sprite[] shrimpSprites; // Drag shrimp sprites here in the Inspector
    public GameObject shrimpObject; // Drag the GameObject with SpriteRenderer here

    [Header("UI Elements")]
    public TMP_Text pointsText;
    public TMP_Text comboText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public AudioClip boo;

    [Header("Game Settings")]
    public float nextAlpha = 0f;
    public float nowAlpha = 1f;
    public float points = 0f;
    public float comboMultiplier = 1.0f;
    public int comboThreshold = 20;

    private GameObject[] boxes;
    private int currentIndex = -1;
    private int nextIndex = -1;
    private int correctStreak = 0;
    private int lastShrimpIndex = -1;
    private SpriteRenderer shrimpRenderer; // Reference to the SpriteRendererBNH 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Initialize boxes array
        boxes = new GameObject[] { QBox, WBox, EBox, RBox };

        // Cache the SpriteRenderer component
        shrimpRenderer = shrimpObject.GetComponent<SpriteRenderer>();

        // Start game
        currentIndex = Random.Range(0, boxes.Length);
        SetBoxAlpha(currentIndex, nowAlpha);
        nextIndex = GetNextIndex(currentIndex);

        SetBoxAlpha(nextIndex, nextAlpha);
        UpdateUI();

        // Start with shrimpSprites[0]
        SetShrimpSprite(0);

        // Start QTE sequence
        StartCoroutine(QTESequence());
    }

    private void Update()
    {

    }


    private IEnumerator QTESequence()
    {
        while (true)
        {
            yield return StartCoroutine(WaitForInput());
            SetBoxAlpha(currentIndex, 0f); // Reset previous box
            SetBoxAlpha(nextIndex, nowAlpha); // Highlight the current box

            currentIndex = nextIndex;
            nextIndex = GetNextIndex(currentIndex);
            SetBoxAlpha(nextIndex, nextAlpha); // Prepare next box

            UpdateUI();
        }
    }


    private IEnumerator WaitForInput()
    {
        bool inputReceived = false;

        while (!inputReceived)
        {
            // Check if the correct key is pressed
            if (currentIndex == 0 && Input.GetKeyDown(KeyCode.Q)) // Prompt is Q, only Q is correct
            {
                inputReceived = true;
                HandleCorrectInput();
            }
            else if (currentIndex == 1 && Input.GetKeyDown(KeyCode.W)) // Prompt is W, only W is correct
            {
                inputReceived = true;
                HandleCorrectInput();
            }
            else if (currentIndex == 2 && Input.GetKeyDown(KeyCode.E)) // Prompt is E, only E is correct
            {
                inputReceived = true;
                HandleCorrectInput();
            }
            else if (currentIndex == 3 && Input.GetKeyDown(KeyCode.R)) // Prompt is R, only R is correct
            {
                inputReceived = true;
                HandleCorrectInput();
            }
            // Handle incorrect inputs for any other keypresses
            else if ((currentIndex == 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))) || // Wrong key when Q is expected
                     (currentIndex == 1 && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))) || // Wrong key when W is expected
                     (currentIndex == 2 && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.R))) || // Wrong key when E is expected
                     (currentIndex == 3 && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E)))) // Wrong key when R is expected
            {
                inputReceived = true;
                HandleIncorrectInput();
            }
            // Ignore all other keys or mouse clicks
            else if (Input.anyKeyDown || Input.GetMouseButtonDown(0)) // Mouse click or any key press
            {
                // Do nothing if it's not one of Q, W, E, or R
                continue;
            }

            yield return null;
        }
    }

    private void SetBoxAlpha(int index, float alpha)
    {
        if (index >= 0 && index < boxes.Length)
        {
            var image = boxes[index].GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }

    private void HandleCorrectInput()
    {
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);

        correctStreak++;
        if (correctStreak % comboThreshold == 0)
            comboMultiplier += 0.5f;

        points += 10 * comboMultiplier;

        // Display a correct shrimp (index >= 2)
        SetShrimpSprite(GetNextCorrectShrimpIndex());

        Debug.Log($"Correct Input! Points: {points}, Multiplier: {comboMultiplier}, Streak: {correctStreak}");
    }

    private void HandleIncorrectInput()
    {
        audioSource.PlayOneShot(boo);

        correctStreak = 0;
        comboMultiplier = 1.0f;

        // Display the incorrect shrimp (index 1)
        SetShrimpSprite(1);

        Debug.Log("Incorrect Input!");
    }

    private void UpdateUI()
    {
        pointsText.text = $"Points: {points}";
        comboText.text = $"Multiplier: {correctStreak} {comboMultiplier:F1}";
    }

    private int GetNextIndex(int current)
    {
        return (current + Random.Range(1, boxes.Length)) % boxes.Length;
    }

    private int GetNextCorrectShrimpIndex()
    {
        int newIndex;
        do
        {
            newIndex = Random.Range(2, shrimpSprites.Length); // Only indices >= 2
        } while (newIndex == lastShrimpIndex);
        return newIndex;
    }

    private void SetShrimpSprite(int index)
    {
        if (shrimpRenderer != null && index >= 0 && index < shrimpSprites.Length)
        {
            shrimpRenderer.sprite = shrimpSprites[index]; // Update the sprite
            lastShrimpIndex = index; // Track the current shrimp
        }
    }

    public void OnDestroy()
    {
        Debug.Log("Stopping all coroutines");
        StopAllCoroutines();
    }
}
