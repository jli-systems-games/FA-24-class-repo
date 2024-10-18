using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stat1Game : MonoBehaviour
{
    public TextMeshProUGUI inputDisplay;
    private string enteredNumber = "";
    private const string targetNumber = "500";

    private AudioSource audioSource;
    public AudioClip buttonSound;
    public AudioClip cookingSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNumberPressed(string number)
    {
        PlayButtonSound();

        if (enteredNumber.Length < 3)
        {
            enteredNumber += number;
            inputDisplay.text = enteredNumber;
        }
    }

    public void OnEnterPressed()
    {
        PlayButtonSound();

        if (enteredNumber == targetNumber)
        {
            PlayCookingSound();
            StartCoroutine(CompleteGameAfterDelay(6f));
        }
        else
        {
            ResetInput();
        }
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }

    private void PlayCookingSound()
    {
        if (audioSource != null && cookingSound != null)
        {
            audioSource.PlayOneShot(cookingSound);
        }
    }

    private void ResetInput()
    {
        enteredNumber = "";
        inputDisplay.text = "";
    }

    private IEnumerator CompleteGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CompleteMiniGame();
    }

    private void CompleteMiniGame()
    {
        ResetInput();
        PetManager.Instance.CompleteMiniGame1();
    }
}
