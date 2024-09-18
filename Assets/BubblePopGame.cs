using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BubblePopGame : MonoBehaviour
{
    public GameObject[] bubbles; 
    public float lightDuration = 1.5f;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI missedText;
    public AudioSource audioSource;
    public AudioClip bubbleClickSound;
    private int bubbleIndex;
    private bool bubbleLit = false;
    private int correctClicks = 0;
    private int missedClicks = 0;

    void Start()
    {
        StartCoroutine(LightUpRandomBubbles());
        UpdateUI();
    }

    IEnumerator LightUpRandomBubbles()
    {
        while (true)
        {
            bubbleIndex = Random.Range(0, bubbles.Length);
            LightBubble(bubbleIndex);
            yield return new WaitForSeconds(lightDuration);
            DimBubble(bubbleIndex);
        }
    }

    void LightBubble(int index)
    {
        bubbles[index].GetComponent<Image>().color = Random.ColorHSV();
        bubbleLit = true;
    }

    void DimBubble(int index)
    {
        if (bubbleLit)
        {
            missedClicks++;
            UpdateUI();
        }
        bubbles[index].GetComponent<Image>().color = Random.ColorHSV();
        bubbleLit = false;
    }

    public void OnBubbleClick(int index)
    {
        if (bubbleLit && index == bubbleIndex)
        {
            correctClicks++; 
            audioSource.PlayOneShot(bubbleClickSound);
            bubbleLit = false; 
            UpdateUI();
            Debug.Log("Correct Bubble clicked!");
        }
    }

    void UpdateUI()
    {
        correctText.text = "Correct: " + correctClicks;
        missedText.text = "Missed: " + missedClicks;
    }
}
