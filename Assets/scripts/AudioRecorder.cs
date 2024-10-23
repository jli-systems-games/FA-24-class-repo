using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro namespace

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
    public Button composeButton;                // Button to start composing (hidden initially)

    public Image[] inputImages;                // Images to show when a note is registered
    private int currentInputIndex = 0;        // Tracks the current input position
    private AudioClip recordedClip;            // The recorded audio clip
    private bool isRecording = false;

    void Start()
    {
        // Add listener to record button to trigger recording
        recordButton.onClick.AddListener(StartRecording);

        // Ensure the composition UI is hidden at the start
        compositionUI.SetActive(false);

        // Ensure the recording text is hidden at the start
        isRecordingText.gameObject.SetActive(false);

        // Add listeners for back and confirm buttons
        backButton.onClick.AddListener(GoBack);
        confirmButton.onClick.AddListener(ConfirmComposition);

        // Ensure back and confirm buttons are hidden at the start
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);

        // Ensure compose button is hidden at the start
        composeButton.gameObject.SetActive(false);

        // Initialize input images to be hidden
        foreach (var img in inputImages)
        {
            img.gameObject.SetActive(false);
        }

        // Add listeners for pitch buttons
        AssignAudioToButtons();
    }

    // Coroutine to handle the recording process
    IEnumerator StartRecordingProcess()
    {
        if (!isRecording)
        {
            isRecording = true;

            // Start recording
            recordedClip = Microphone.Start(null, false, 5, 44100);
            isRecordingText.gameObject.SetActive(true);  // Show "Recording..." text immediately after starting recording

            // Wait for the duration of the recording
            yield return new WaitForSeconds(5f);

            // Stop recording after 5 seconds
            StopRecording();
        }
    }

    // Method to start the recording coroutine
    void StartRecording()
    {
        StartCoroutine(StartRecordingProcess());
    }

    // Method to stop recording and switch UI
    void StopRecording()
    {
        if (isRecording)
        {
            Microphone.End(null);                    // Stop recording
            audioSource.clip = recordedClip;         // Assign the recorded clip
            isRecording = false;
            isRecordingText.gameObject.SetActive(false);  // Hide the "Recording..." text

            // Close the recording UI and open the composition UI
            recordingUI.SetActive(false);
            OpenInstrumentUICanvas();
        }
    }

    // Function to open the composition UI after recording is finished
    void OpenInstrumentUICanvas()
    {
        compositionUI.SetActive(true); // Open the UI for composing music
        backButton.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }

    // Method to handle the back button functionality
    void GoBack()
    {
        // Hide the composition UI and show the recording UI again
        compositionUI.SetActive(false);
        recordingUI.SetActive(true);
    }

    // Method to handle the confirm button functionality
    void ConfirmComposition()
    {
        // Hide back and confirm buttons
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);

        // Show the compose button 
        composeButton.gameObject.SetActive(true);
        //StartCompositionMode();
    }

    // Method to start composition mode when compose button is clicked
    public void StartCompositionMode()
    {
        Debug.Log("Entering Composition Mode");
        composeButton.gameObject.SetActive(false); // Hide compose button
        currentInputIndex = 0; // Reset input index

        // Hide all input images initially
        foreach (var img in inputImages)
        {
            img.gameObject.SetActive(false);
        }

        // Add listeners for pitch buttons to allow note input
        foreach (var pitchButton in pitchButtons)
        {
            pitchButton.onClick.AddListener(() => RegisterNoteInput(pitchButton));
        }
    }

    // Method to register note input
    void RegisterNoteInput(Button pitchButton)
    {
        if (currentInputIndex < inputImages.Length)
        {
            inputImages[currentInputIndex].gameObject.SetActive(true); // Show corresponding image
            currentInputIndex++;

            // Play the corresponding audio for the button pressed
            float pitch = 1f + (System.Array.IndexOf(pitchButtons, pitchButton) * 0.1f);
            PlayRecordedAudio(pitch);

            // Check if the maximum notes have been registered
            if (currentInputIndex >= inputImages.Length)
            {
                StartCoroutine(PlayComposition());
            }
        }
    }

    // Coroutine to play the composition in order
    private IEnumerator PlayComposition()
    {
        yield return new WaitForSeconds(1f); // Optional delay before starting playback

        // Play the recorded audio clips in the order registered
        foreach (var img in inputImages)
        {
            if (img.gameObject.activeSelf)
            {
                float pitch = 1f + (System.Array.IndexOf(inputImages, img) * 0.1f);
                PlayRecordedAudio(pitch);
                yield return new WaitForSeconds(1f); // Adjust the delay between notes as needed
            }
        }
    }

    // Assign the recorded audio clip to the buttons and modify the pitch
    void AssignAudioToButtons()
    {
        if (pitchButtons.Length == 5) // Ensure there are 5 buttons
        {
            for (int i = 0; i < pitchButtons.Length; i++)
            {
                // Calculate pitch for each button (index 0 has pitch 1, index 4 has pitch 1.4, etc.)
                float pitch = 1f + (i * 0.1f);  // Increase pitch progressively (1.0, 1.1, 1.2, ...)

                // Assign behavior to each button
                int buttonIndex = i; // Capture the index for use in the lambda
                pitchButtons[buttonIndex].onClick.AddListener(() =>
                {
                    PlayRecordedAudio(pitch);  // Play the recorded audio with the corresponding pitch
                });
            }
        }
    }

    // Play the recorded audio with a specific pitch
    void PlayRecordedAudio(float pitch)
    {
        if (recordedClip != null)
        {
            audioSource.pitch = pitch;   // Adjust the pitch
            audioSource.Play();          // Play the recorded clip with the set pitch
        }
    }
}
