using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioRecorder : MonoBehaviour
{
    public AudioSource audioSource;            // AudioSource to play the recorded sound
    public Button recordButton;                // UI button to start recording
    public GameObject recordingUI;             // The UI panel for recording
    public GameObject compositionUI;           // The UI panel for composing
    public Button[] pitchButtons;              // Array of buttons for different pitches
    public TextMeshProUGUI isRecordingText;    // TextMeshProUGUI to show "Recording..." when recording

    public Button backButton;                  // Button to go back to the recording UI
    public Button confirmButton;               // Button to confirm the composition
    public Button composeButton;               // Button to start composing (hidden initially)
    public Button deleteButton;                // New button for deleting the last note

    public Image[] inputImages;                // Images to show when a note is registered
    private int currentInputIndex = 0;         // Tracks the current input position
    private AudioClip recordedClip;            // The recorded audio clip
    private bool isRecording = false;

    public GameObject blank; 
    public GameObject tryText; 

    // Array to store the sequence of player input
    private int[] noteSequence = new int[16];  // Array to store button indices for 16 notes

    // Fixed frequency array for pitches
    private float[] pitches = new float[] { 220f, 261.63f, 329.63f, 392.00f, 493.88f };

    // Define color array corresponding to pitch (low to high = dark to light, e.g., black to white)
    private Color[] noteColors = new Color[] {
        Color.black,            // Corresponds to lowest pitch
        new Color(0.25f, 0.25f, 0.25f), // Dark gray
        Color.gray,             // Mid gray
        new Color(0.75f, 0.75f, 0.75f), // Light gray
        new Color(0.9f, 0.9f, 0.9f)  
    };

    void Start()
    {
        recordButton.onClick.AddListener(StartRecording);
        compositionUI.SetActive(false);
        isRecordingText.gameObject.SetActive(false);
        backButton.onClick.AddListener(GoBack);
        confirmButton.onClick.AddListener(ConfirmComposition);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        composeButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);

        blank.SetActive(false);

        foreach (var img in inputImages)
        {
            img.gameObject.SetActive(false);
        }

        // Add listener for the delete button
        deleteButton.onClick.AddListener(DeleteLastNote);

        AssignAudioToButtons();
    }

    IEnumerator StartRecordingProcess()
    {
        if (!isRecording)
        {
            isRecording = true;
            recordedClip = Microphone.Start(null, false, 3, 44100);
            isRecordingText.gameObject.SetActive(true);  // Show "Recording..." text

            yield return new WaitForSeconds(3f);         // Wait for recording to complete
            StopRecording();
        }
    }

    void StartRecording()
    {
        StartCoroutine(StartRecordingProcess());
    }

    void StopRecording()
    {
        if (isRecording)
        {
            Microphone.End(null);
            audioSource.clip = recordedClip;
            isRecording = false;
            isRecordingText.gameObject.SetActive(false);

            recordingUI.SetActive(false);
            OpenInstrumentUICanvas();
        }
    }

    void OpenInstrumentUICanvas()
    {
        compositionUI.SetActive(true);
        backButton.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }

    void GoBack()
    {
        compositionUI.SetActive(false);
        recordingUI.SetActive(true);
    }

    void ConfirmComposition()
    {
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        composeButton.gameObject.SetActive(true);
        tryText.SetActive(false);
    }

    public void StartCompositionMode()
    {
        composeButton.gameObject.SetActive(false);
        currentInputIndex = 0;
        deleteButton.gameObject.SetActive(true);
        blank.SetActive(true);

        foreach (var img in inputImages)
        {
            img.gameObject.SetActive(false);
        }

        foreach (var pitchButton in pitchButtons)
        {
            pitchButton.onClick.AddListener(() => RegisterNoteInput(pitchButton));
        }
    }

    void RegisterNoteInput(Button pitchButton)
    {
        if (currentInputIndex < noteSequence.Length)
        {
            inputImages[currentInputIndex].gameObject.SetActive(true);
            int index = System.Array.IndexOf(pitchButtons, pitchButton);

            // Ensure the index is within bounds of the pitches array
            if (index >= 0 && index < pitches.Length)
            {
                noteSequence[currentInputIndex] = index;  // Store the index of the pressed button

                // Set the corresponding image color based on the note
                inputImages[currentInputIndex].color = noteColors[index];  // Change image color
            }

            currentInputIndex++;
            if (currentInputIndex >= noteSequence.Length)
            {
                StartCoroutine(PlayComposition());
            }
        }
    }

    // New method to handle deleting the last registered note
    void DeleteLastNote()
    {
        if (currentInputIndex > 0)
        {
            currentInputIndex--; // Move back one step in the input sequence
            inputImages[currentInputIndex].gameObject.SetActive(false); // Hide the last registered image
        }
    }

    private IEnumerator PlayComposition()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < currentInputIndex; i++)
        {
            PlayRecordedAudio(pitches[noteSequence[i]]);  // Play the pitch based on the stored index
            yield return new WaitForSeconds(1f);
        }
    }

    void AssignAudioToButtons()
    {
        for (int i = 0; i < pitchButtons.Length; i++)
        {
            int buttonIndex = i;
            pitchButtons[buttonIndex].onClick.AddListener(() =>
            {
                PlayRecordedAudio(pitches[buttonIndex]);
            });
        }
    }

    // Apply a multiplier to the pitch to make the differences more pronounced
    void PlayRecordedAudio(float pitch)
    {
        if (recordedClip != null)
        {
            audioSource.pitch = pitch / 250f;  // Multiply pitch for more noticeable differences
            audioSource.Play();
        }
    }
}
